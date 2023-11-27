using Microsoft.Extensions.Caching.Memory;
using SpendWise.Shared.Abstraction.Messaging;


namespace SpendWise.Shared.Infrastructure.Messaging.Contexts;

public class MessageContextRegistry(IMemoryCache cache) : IMessageContextRegistry
{
    public void Set(IMessage message, IMessageContext context)
        => cache.Set(message, context, new MemoryCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(1)
        });
}