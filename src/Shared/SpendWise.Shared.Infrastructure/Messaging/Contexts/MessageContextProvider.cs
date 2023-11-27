using Microsoft.Extensions.Caching.Memory;
using SpendWise.Shared.Abstraction.Messaging;

namespace SpendWise.Shared.Infrastructure.Messaging.Contexts;

public class MessageContextProvider(IMemoryCache cache) : IMessageContextProvider
{
    public IMessageContext Get(IMessage message) => cache.Get<IMessageContext>(message);
}