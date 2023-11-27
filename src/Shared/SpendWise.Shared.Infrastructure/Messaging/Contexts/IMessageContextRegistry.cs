using SpendWise.Shared.Abstraction.Messaging;

namespace SpendWise.Shared.Infrastructure.Messaging.Contexts;

public interface IMessageContextRegistry
{
    void Set(IMessage message, IMessageContext context);
}