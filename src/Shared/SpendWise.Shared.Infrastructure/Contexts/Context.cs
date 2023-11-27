using Microsoft.AspNetCore.Http;
using SpendWise.Shared.Abstraction.Contexts;
using SpendWise.Shared.Infrastructure.Api;

namespace SpendWise.Shared.Infrastructure.Contexts;

public class Context(Guid? correlationId, string traceId, IIdentityContext identity = null, string ipAddress = null,
        string userAgent = null)
    : IContext
{
    public Guid RequestId { get; } = Guid.NewGuid();
    public Guid CorrelationId { get; } = correlationId ?? Guid.NewGuid();
    public string TraceId { get; } = traceId;
    public string IpAddress { get; } = ipAddress;
    public string UserAgent { get; } = userAgent;
    public IIdentityContext Identity { get; } = identity ?? IdentityContext.Empty;

    public Context() : this(Guid.NewGuid(), $"{Guid.NewGuid():N}", null)
    {
    }

    public Context(HttpContext context) : this(context.TryGetCorrelationId(), context.TraceIdentifier,
        new IdentityContext(context.User), context.GetUserIpAddress(),
        context.Request.Headers["user-agent"])
    {
    }

    public static IContext Empty => new Context();
}