using System.Runtime.CompilerServices;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SpendWise.Modules.Users.Core.Users.Commands.Public.ChangePassword;
using SpendWise.Modules.Users.Core.Users.Commands.Public.SignIn;
using SpendWise.Modules.Users.Core.Users.Commands.Public.SignUp;
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
            .AddInitializer<UsersInitializer>()
            .AddValidators();
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
        => services
            .AddScoped<IValidator<ChangePasswordCommand>, ChangePasswordValidator>()
            .AddScoped<IValidator<SignInCommand>, SignInValidator>()
            .AddScoped<IValidator<SignUpCommand>, SingUpValidator>();
}