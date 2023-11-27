using SpendWise.Shared.Abstraction.Contexts;

namespace SpendWise.Shared.Abstraction.Messaging;

public interface IMessageContext
{
    public Guid MessageId { get; }
    public IContext Context { get; }
}