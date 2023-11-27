using Humanizer;
using Microsoft.Extensions.Logging;
using SpendWise.Shared.Abstraction.Contexts;
using SpendWise.Shared.Abstraction.Queries;

namespace SpendWise.Shared.Infrastructure.Logging.Decorators;

[Decorator]
internal sealed class LoggingQueryHandlerDecorator<TQuery, TResult>(IQueryHandler<TQuery, TResult> handler,
        IContext context, ILogger<LoggingQueryHandlerDecorator<TQuery, TResult>> logger)
    : IQueryHandler<TQuery, TResult>
    where TQuery : class, IQuery<TResult>
{
    public async Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default)
    {
        var module = query.GetModuleName();
        var name = query.GetType().Name.Underscore();
        var requestId = context.RequestId;
        var correlationId = context.CorrelationId;
        var traceId = context.TraceId;
        var userId = context.Identity?.Id;
        logger.LogInformation("Handling a query: {Name} ({Module}) [Request ID: {RequestId}, Correlation ID: {CorrelationId}, Trace ID: '{TraceId}', User ID: '{UserId}]...",
            name, module, requestId, correlationId, traceId, userId);
        var result = await handler.HandleAsync(query, cancellationToken);
        logger.LogInformation("Handled a query: {Name} ({Module}) [Request ID: {RequestId}, Correlation ID: {CorrelationId}, Trace ID: '{TraceId}', User ID: '{UserId}]",
            name, module, requestId, correlationId, traceId, userId);

        return result;
    }
}