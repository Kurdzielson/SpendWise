using Humanizer;
using Microsoft.Extensions.Logging;
using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Contexts;
using SpendWise.Shared.Abstraction.Messaging;

namespace SpendWise.Shared.Infrastructure.Logging.Decorators;

[Decorator]
internal sealed class LoggingCommandHandlerDecorator<T>(ICommandHandler<T> handler,
        IMessageContextProvider messageContextProvider,
        IContext context, ILogger<LoggingCommandHandlerDecorator<T>> logger)
    : ICommandHandler<T>
    where T : class, ICommand
{
    private readonly ILogger<LoggingCommandHandlerDecorator<T>> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task HandleAsync(T command, CancellationToken cancellationToken = default)
    {
        var module = command.GetModuleName();
        var name = command.GetType().Name.Underscore();
        var messageContext = messageContextProvider.Get(command);
        var requestId = context.RequestId;
        var traceId = context.TraceId;
        var userId = context.Identity?.Id;
        var messageId = messageContext?.MessageId;
        var correlationId = messageContext?.Context.CorrelationId ?? context.CorrelationId;
        _logger.LogInformation("Handling a command: {Name} ({Module}) [Request ID: {RequestId}, Message ID: {MessageId}, Correlation ID: {CorrelationId}, Trace ID: '{TraceId}', User ID: '{UserId}]'...",
            name, module, requestId, messageId, correlationId, traceId, userId);
        await handler.HandleAsync(command, cancellationToken);
        _logger.LogInformation("Handled a command: {Name} ({Module}) [Request ID: {RequestId}, Message ID: {MessageId}, Correlation ID: {CorrelationId}, Trace ID: '{TraceId}', User ID: '{UserId}']",
            name, module, requestId, messageId, correlationId, traceId, userId);
    }
}