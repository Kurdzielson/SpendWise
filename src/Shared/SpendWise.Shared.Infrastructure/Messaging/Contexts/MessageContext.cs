using SpendWise.Shared.Abstraction.Contexts;
using SpendWise.Shared.Abstraction.Messaging;

namespace SpendWise.Shared.Infrastructure.Messaging.Contexts;

public class MessageContext(Guid messageId, IContext context) : IMessageContext
{
    public Guid MessageId { get; } = messageId;
    public IContext Context { get; } = context;
}