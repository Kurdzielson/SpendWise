using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SpendWise.Modules.Users.Core.Users.DAL;
using SpendWise.Modules.Users.Core.Users.DAL.Repositories;
using SpendWise.Modules.Users.Core.Users.Domain.Entities;
using SpendWise.Modules.Users.Core.Users.Domain.Repositories;
using SpendWise.Modules.Users.Core.Users.Domain.Services;
using SpendWise.Shared.Infrastructure;
using SpendWise.Shared.Infrastructure.Postgres;

[assembly: InternalsVisibleTo("SpendWise.Modules.Users.Api")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]


namespace SpendWise.Modules.Users.Core.Users;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        var registrationOptions = services.GetOptions<RegistrationOptions>("users:registration");
        services.AddSingleton(registrationOptions);

        return services
            .AddSingleton<IUserRequestStorage, UserRequestStorage>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddPostgres<UsersWriteDbContext>()
            .AddPostgres<UsersReadDbContext>()
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddUnitOfWork<UsersUnitOfWork>()
            .AddInitializer<UsersInitializer>();
    }
}