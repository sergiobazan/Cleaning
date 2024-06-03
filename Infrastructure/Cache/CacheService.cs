using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Infrastructure.Cache;

internal class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;

    private static readonly JsonSerializerSettings SerializerSettings = new()
    {
        ContractResolver = new PrivateConstructorContractResolver(),
    };


    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T?> GetAsync<T>(string cacheKey, Func<Task<T?>> factory) where T : class
    {
        T? cachedValue = await GetAsync<T>(cacheKey);

        if (cachedValue is not null)
        {
            return cachedValue;
        }

        cachedValue = await factory();

        await SetAsync(cacheKey, cachedValue);

        return cachedValue;
    }

    public async Task<T?> GetAsync<T>(string cacheKey) where T : class
    {
        string? cacheEntity = await _cache.GetStringAsync(cacheKey);

        if (cacheEntity is null) return null;

        T? entity = JsonConvert.DeserializeObject<T>(cacheEntity, SerializerSettings);

        return entity;
    }

    public async Task SetAsync<T>(string cacheKey, T? value) where T : class
    {
       if (value is null) return;

       await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(value));
    }

}
