using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Events;
using SpendWise.Shared.Abstraction.Queries;

namespace SpendWise.Shared.Abstraction.Dispatchers;

public interface IDispatcher
{
    Task SendAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand;

    Task<TResult> SendAsync<T, TResult>(T command, CancellationToken cancellationToken = default)
        where T : class, ICommand<TResult> where TResult : class;

    Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent;
    Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
}