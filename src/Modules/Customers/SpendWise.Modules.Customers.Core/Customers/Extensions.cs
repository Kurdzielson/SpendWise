using System.Runtime.CompilerServices;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SpendWise.Modules.Customers.Core.Customers.Commands.CompleteCustomer;
using SpendWise.Modules.Customers.Core.Customers.Commands.CreateCustomer;
using SpendWise.Modules.Customers.Core.Customers.Commands.LockCustomer;
using SpendWise.Modules.Customers.Core.Customers.Commands.UnlockCustomer;
using SpendWise.Modules.Customers.Core.Customers.Commands.UpdateCustomer;
using SpendWise.Modules.Customers.Core.Customers.Commands.VerifyCustomer;
using SpendWise.Modules.Customers.Core.Customers.DAL;
using SpendWise.Modules.Customers.Core.Customers.DAL.Repositories;
using SpendWise.Modules.Customers.Core.Customers.Domain.Repositories;
using SpendWise.Modules.Customers.Core.Users.Clients;
using SpendWise.Shared.Infrastructure.Postgres;

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
            .AddUnitOfWork<CustomersUnitOfWork>()
            .AddValidators();

    private static IServiceCollection AddValidators(this IServiceCollection services)
        => services
            .AddScoped<IValidator<CompleteCustomerCommand>, CompleteCustomerValidator>()
            .AddScoped<IValidator<CreateCustomerCommand>, CreateCustomerValidator>()
            .AddScoped<IValidator<LockCustomerCommand>, LockCustomerValidator>()
            .AddScoped<IValidator<UnlockCustomerCommand>, UnlockCustomerValidator>()
            .AddScoped<IValidator<UpdateCustomerCommand>, UpdateCustomerValidator>()
            .AddScoped<IValidator<VerifyCustomerCommand>, VerifyCustomerValidator>();
}