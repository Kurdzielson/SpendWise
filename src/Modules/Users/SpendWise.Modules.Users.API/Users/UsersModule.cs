using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SpendWise.Modules.Users.API.Users.Endpoints.Account;
using SpendWise.Modules.Users.Core.Users;
using SpendWise.Modules.Users.Core.Users.DTO;
using SpendWise.Modules.Users.Core.Users.Queries.GetUserByEmail;
using SpendWise.Shared.Abstraction.Modules;
using SpendWise.Shared.Abstraction.Queries;
using SpendWise.Shared.Infrastructure.Modules;

namespace SpendWise.Modules.Users.API.Users;

internal class UsersModule : IModule
{
    public string Name { get; } = "Users";
    public const string Policy = "users";

    public IEnumerable<string> Policies { get; } = new[]
    {
        Policy
    };

    public void Register(IServiceCollection services)
        => services.AddCore();


    public void Use(IApplicationBuilder app)
    {
        app.UseModuleRequests()
            .Subscribe<GetUserByEmailQuery, UserDetailsDto>("users/get",
                (query, serviceProvider, cancellationToken) =>
                    serviceProvider.GetRequiredService<IQueryDispatcher>().QueryAsync(query, cancellationToken));
    }
}