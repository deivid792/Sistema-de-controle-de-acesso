using Microsoft.Extensions.Caching.Memory;
using visitor.cfg.aplication.Interface.Cache;

namespace Visitor.Cfg.Infrastructure.Cache
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _defaultExpiration;

        public MemoryCacheService(IMemoryCache cache, TimeSpan defaultExpiration)
        {
            _cache = cache;
            _defaultExpiration = defaultExpiration;
        }

        public Task<T?> GetAsync<T>(string key)
        {
            _cache.TryGetValue(key, out T? value);
            return Task.FromResult(value);
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? _defaultExpiration
            };

            _cache.Set(key, value, options);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(string key)
        {
            _cache.Remove(key);
            return Task.CompletedTask;
        }
    }
}
