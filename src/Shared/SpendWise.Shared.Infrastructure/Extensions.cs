using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SpendWise.Shared.Abstraction.Cookies;
using SpendWise.Shared.Abstraction.Dispatchers;
using SpendWise.Shared.Abstraction.Modules;
using SpendWise.Shared.Abstraction.Storage;
using SpendWise.Shared.Abstraction.Time;
using SpendWise.Shared.Infrastructure.Api;
using SpendWise.Shared.Infrastructure.Auth;
using SpendWise.Shared.Infrastructure.Commands;
using SpendWise.Shared.Infrastructure.Contexts;
using SpendWise.Shared.Infrastructure.Contracts;
using SpendWise.Shared.Infrastructure.Cookies;
using SpendWise.Shared.Infrastructure.Dispatchers;
using SpendWise.Shared.Infrastructure.Events;
using SpendWise.Shared.Infrastructure.Exceptions;
using SpendWise.Shared.Infrastructure.Kernel;
using SpendWise.Shared.Infrastructure.Logging;
using SpendWise.Shared.Infrastructure.Messaging;
using SpendWise.Shared.Infrastructure.Messaging.Outbox;
using SpendWise.Shared.Infrastructure.Modules;
using SpendWise.Shared.Infrastructure.Postgres;
using SpendWise.Shared.Infrastructure.Queries;
using SpendWise.Shared.Infrastructure.Serialization;
using SpendWise.Shared.Infrastructure.Services;
using SpendWise.Shared.Infrastructure.Storage;
using SpendWise.Shared.Infrastructure.Swagger;
using SpendWise.Shared.Infrastructure.Time;

namespace SpendWise.Shared.Infrastructure;

public static class Extensions
{
    private const string CorrelationIdKey = "correlation-id";

    public static IServiceCollection AddInitializer<T>(this IServiceCollection services) where T : class, IInitializer
        => services.AddTransient<IInitializer, T>();

    public static IServiceCollection AddModularInfrastructure(this IServiceCollection services,
        IList<Assembly> assemblies, IList<IModule> modules)
    {
        var disabledModules = new List<string>();
        using (var serviceProvider = services.BuildServiceProvider())
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            foreach (var (key, value) in configuration.AsEnumerable())
            {
                if (!key.Contains(":module:enabled"))
                {
                    continue;
                }

                if (!bool.Parse(value))
                {
                    disabledModules.Add(key.Split(":")[0]);
                }
            }
        }

        services.AddCorsPolicy();
        services.AddSwaggerGen(swagger =>
        {
            swagger.EnableAnnotations();
            swagger.CustomSchemaIds(x => x.FullName);
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Modular API",
                Version = "v1"
            });
            swagger.OperationFilter<AddOperationIdFilter>();
        });

        var appOptions = services.GetOptions<AppOptions>("app");
        services.AddSingleton(appOptions);
        services.AddScoped<ICookieService, CookieService>();

        services.AddMemoryCache();
        services.AddHttpClient();
        services.AddSingleton<IRequestStorage, RequestStorage>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IJsonSerializer, SystemTextJsonSerializer>();
        services.AddModuleInfo(modules);
        services.AddModuleRequests(assemblies);
        services.AddAuth(modules);
        services.AddErrorHandling();
        services.AddContext();
        services.AddCommands(assemblies);
        services.AddQueries(assemblies);
        services.AddEvents(assemblies);
        services.AddDomainEvents(assemblies);
        services.AddMessaging();
        services.AddSingleton<IClock, UtcClock>();
        services.AddSingleton<IDispatcher, InMemoryDispatcher>();
        services.AddLoggingDecorators();
        services.AddPostgres();
        services.AddOutbox();
        services.AddHostedService<DbContextAppInitializer>();
        services.AddLogging();
        services.AddContracts();
        services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                var removedParts = new List<ApplicationPart>();
                foreach (var disabledModule in disabledModules)
                {
                    var parts = manager.ApplicationParts.Where(x => x.Name.Contains(disabledModule,
                        StringComparison.InvariantCultureIgnoreCase));
                    removedParts.AddRange(parts);
                }

                foreach (var part in removedParts)
                {
                    manager.ApplicationParts.Remove(part);
                }

                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });
        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

        return services;
    }

    public static IApplicationBuilder UseModularInfrastructure(this IApplicationBuilder app)
    {
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.All
        });
        app.UseCors("cors");
        app.UseCorrelationId();
        app.UseErrorHandling();
        app.UseSwagger(swagger => { swagger.SerializeAsV2 = true; });
        app.UseReDoc(reDoc =>
        {
            reDoc.RoutePrefix = "docs";
            reDoc.SpecUrl("/swagger/v1/swagger.json");
            reDoc.DocumentTitle = "Modular API";
        });
        app.UseSwaggerUI(ui =>
        {
            ui.RoutePrefix = "swagger";
            ui.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            ui.DocumentTitle = "Modular API";
        });
        app.UseAuth();
        app.UseContext();
        app.UseLogging();
        app.UseRouting();
        app.UseAuthorization();

        return app;
    }

    public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        return configuration.GetOptions<T>(sectionName);
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }

    public static string GetModuleName(this object value)
        => value?.GetType().GetModuleName() ?? string.Empty;

    public static string GetModuleName(this Type type, string namespacePart = "Modules", int splitIndex = 2)
    {
        if (type?.Namespace is null)
        {
            return string.Empty;
        }

        return type.Namespace.Contains(namespacePart)
            ? type.Namespace.Split(".")[splitIndex].ToLowerInvariant()
            : string.Empty;
    }

    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
        => app.Use((ctx, next) =>
        {
            ctx.Items.Add(CorrelationIdKey, Guid.NewGuid());
            return next();
        });

    public static Guid? TryGetCorrelationId(this HttpContext context)
        => context.Items.TryGetValue(CorrelationIdKey, out var id) ? (Guid)id : null;
}