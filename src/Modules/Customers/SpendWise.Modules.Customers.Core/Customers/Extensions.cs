using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using SpendWise.Modules.Customers.Core.Customers.DAL;
using SpendWise.Modules.Customers.Core.Customers.DAL.Repositories;
using SpendWise.Modules.Customers.Core.Customers.Domain.Repositories;
using SpendWise.Modules.Customers.Core.Users.Clients;
using SpendWise.Shared.Abstraction.Time;
using SpendWise.Shared.Infrastructure.Postgres;
using SpendWise.Shared.Infrastructure.Time;

[assembly: InternalsVisibleTo("SpendWise.Modules.Customers.Api")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace SpendWise.Modules.Customers.Core.Customers;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
        => services
            .AddScoped<ICustomerRepository, CustomerRepository>()
            .AddSingleton<IUserApiClient, UserApiClient>()
            .AddPostgres<CustomersWriteDbContext>()
            .AddPostgres<CustomersReadDbContext>()
            .AddUnitOfWork<CustomersUnitOfWork>();
}