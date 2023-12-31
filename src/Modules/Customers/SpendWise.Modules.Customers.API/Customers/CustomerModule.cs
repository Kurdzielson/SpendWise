using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SpendWise.Modules.Customers.Core.Customers;
using SpendWise.Shared.Abstraction.Modules;

namespace SpendWise.Modules.Customers.API.Customers;

internal class CustomerModule : IModule
{
    public string Name { get; }

    public const string Policy = "customers";
    public IEnumerable<string> Policies { get; } = new[]
    {
        Policy
    };
    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}