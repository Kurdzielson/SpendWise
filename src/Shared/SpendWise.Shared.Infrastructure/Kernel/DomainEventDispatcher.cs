using Microsoft.Extensions.DependencyInjection;
using SpendWise.Shared.Abstraction.Kernel;

namespace SpendWise.Shared.Infrastructure.Kernel;

public sealed class DomainEventDispatcher(IServiceProvider serviceProvider) : IDomainEventDispatcher
{
    public Task DispatchAsync(IDomainEvent @event, CancellationToken cancellationToken = default)
        => DispatchAsync(cancellationToken, @event);

    public Task DispatchAsync(IDomainEvent[] events, CancellationToken cancellationToken = default)
        => DispatchAsync(cancellationToken, events);

    private async Task DispatchAsync(CancellationToken cancellationToken, params IDomainEvent[] events)
    {
        if (events is null || !events.Any())
        {
            return;
        }

        using var scope = serviceProvider.CreateScope();
        foreach (var @event in events)
        {
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());
            var handlers = scope.ServiceProvider.GetServices(handlerType);

            var tasks = handlers.Select(x => (Task)handlerType
                .GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync))
                ?.Invoke(x, new object[] { @event, cancellationToken }));

            await Task.WhenAll(tasks);
        }
    }
}