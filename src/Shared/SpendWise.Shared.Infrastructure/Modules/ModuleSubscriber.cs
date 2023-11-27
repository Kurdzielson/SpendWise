using Microsoft.Extensions.DependencyInjection;
using SpendWise.Shared.Abstraction.Modules;

namespace SpendWise.Shared.Infrastructure.Modules;

internal sealed class ModuleSubscriber(IModuleRegistry moduleRegistry, IServiceProvider serviceProvider)
    : IModuleSubscriber
{
    public IModuleSubscriber Subscribe<TRequest, TResponse>(string path,
        Func<TRequest, IServiceProvider, CancellationToken, Task<TResponse>> action)
        where TRequest : class where TResponse : class
    {
        moduleRegistry.AddRequestAction(path, typeof(TRequest), typeof(TResponse),
            async (request, cancellationToken) =>
            {
                using var scope = serviceProvider.CreateScope();
                return await action((TRequest) request, scope.ServiceProvider, cancellationToken);
            });

        return this;
    }
}