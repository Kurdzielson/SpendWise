using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SpendWise.Modules.Expenses.Application;
using SpendWise.Modules.Expenses.Core;
using SpendWise.Modules.Expenses.Infrastructure;
using SpendWise.Shared.Abstraction.Modules;

namespace SpendWise.Modules.Expenses.API;

internal class ExpensesModule : IModule
{
    public string Name { get; } = "Expenses";

    public void Register(IServiceCollection services)
    {
        services.AddCore();
        services.AddApplication();
        services.AddInfrastructure();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}