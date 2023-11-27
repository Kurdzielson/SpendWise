using Microsoft.Extensions.DependencyInjection;
using SpendWise.Shared.Abstraction.Events;

namespace SpendWise.Shared.Infrastructure.Events;

internal sealed class EventDispatcher(IServiceProvider serviceProvider) : IEventDispatcher
{
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : class, IEvent
    {
        using var scope = serviceProvider.CreateScope();
        var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();
        var tasks = handlers.Select(x => x.HandleAsync(@event, cancellationToken));
        await Task.WhenAll(tasks);
    }
}