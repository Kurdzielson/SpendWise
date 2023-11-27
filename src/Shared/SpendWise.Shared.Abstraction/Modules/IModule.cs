using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SpendWise.Shared.Abstraction.Modules;

public interface IModule
{
    string Name { get; }
    IEnumerable<string> Policies => null;
    void Register(IServiceCollection services);
    void Use(IApplicationBuilder app);
}