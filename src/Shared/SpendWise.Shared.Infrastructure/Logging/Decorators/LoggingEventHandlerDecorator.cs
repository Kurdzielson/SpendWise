using Humanizer;
using Microsoft.Extensions.Logging;
using SpendWise.Shared.Abstraction.Contexts;
using SpendWise.Shared.Abstraction.Events;
using SpendWise.Shared.Abstraction.Messaging;

namespace SpendWise.Shared.Infrastructure.Logging.Decorators;

[Decorator]
internal sealed class LoggingEventHandlerDecorator<T>(IEventHandler<T> handler,
        IMessageContextProvider messageContextProvider,
        IContext context, ILogger<LoggingEventHandlerDecorator<T>> logger)
    : IEventHandler<T>
    where T : class, IEvent
{
    private readonly ILogger<LoggingEventHandlerDecorator<T>> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task HandleAsync(T @event, CancellationToken cancellationToken = default)
    {
        var module = @event.GetModuleName();
        var name = @event.GetType().Name.Underscore();
        var messageContext = messageContextProvider.Get(@event);
        var requestId = context.RequestId;
        var traceId = context.TraceId;
        var userId = context.Identity?.Id;
        var messageId = messageContext?.MessageId;
        var correlationId = messageContext?.Context.CorrelationId ?? context.CorrelationId;
        _logger.LogInformation("Handling an event: {Name} ({Module}) [Request ID: {RequestId}, Message ID: {MessageId}, Correlation ID: {CorrelationId}, Trace ID: '{TraceId}', User ID: '{UserId}]...",
            name, module, requestId, messageId, correlationId, traceId, userId);
        await handler.HandleAsync(@event, cancellationToken);
        _logger.LogInformation("Handled an event: {Name} ({Module}) [Request ID: {RequestId}, Message ID: {MessageId}, Correlation ID: {CorrelationId}, Trace ID: '{TraceId}', User ID: '{UserId}]",
            name, module, requestId, messageId, correlationId, traceId, userId);
    }
}