using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Dispatchers;
using SpendWise.Shared.Abstraction.Events;
using SpendWise.Shared.Abstraction.Queries;

namespace SpendWise.Shared.Infrastructure.Dispatchers;

internal sealed class InMemoryDispatcher(ICommandDispatcher commandDispatcher, IEventDispatcher eventDispatcher,
        IQueryDispatcher queryDispatcher)
    : IDispatcher
{
    public Task SendAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand
        => commandDispatcher.SendAsync(command, cancellationToken);

    public Task<TResult> SendAsync<T, TResult>(T command, CancellationToken cancellationToken = default)
        where T : class, ICommand<TResult> where TResult : class
        => commandDispatcher.SendAsync<T, TResult>(command, cancellationToken);

    public Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent
        => eventDispatcher.PublishAsync(@event, cancellationToken);

    public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
        => queryDispatcher.QueryAsync(query, cancellationToken);
}