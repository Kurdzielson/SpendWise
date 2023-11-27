using SpendWise.Shared.Infrastructure;
using SpendWise.Shared.Infrastructure.Contracts;
using SpendWise.Shared.Infrastructure.Logging;
using SpendWise.Shared.Infrastructure.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureModules();
var assemblies = ModuleLoader.LoadAssemblies(builder.Configuration, "SpendWise.Modules.");
var modules = ModuleLoader.LoadModules(assemblies);

builder.Services.AddModularInfrastructure(assemblies, modules);
foreach (var module in modules)
    module.Register(builder.Services);

builder.Host.UseLogging();

var app = builder.Build();

app.Logger.LogInformation($"Modules: {string.Join(", ", modules.Select(q => q.Name))}");

app.UseModularInfrastructure();
foreach (var module in modules)
    module.Use(app);

app.ValidateContracts(assemblies);

app.MapControllers();
app.MapGet("/", () => "SpendWise API!");
app.MapModuleInfo();

app.Run();