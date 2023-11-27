using Microsoft.Extensions.Caching.Memory;
using SpendWise.Shared.Abstraction.Storage;

namespace SpendWise.Shared.Infrastructure.Storage;

public class RequestStorage(IMemoryCache cache) : IRequestStorage
{
    public void Set<T>(string key, T value, TimeSpan? duration = null) 
        => cache.Set(key, value, duration ?? TimeSpan.FromSeconds(5));

    public T Get<T>(string key) => cache.Get<T>(key);
}