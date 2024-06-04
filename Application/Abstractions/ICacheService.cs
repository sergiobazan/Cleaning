namespace Application.Abstractions;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string cacheKey) where T : class;
    Task SetAsync<T>(string cacheKey, T? value) where T : class;
    Task<T?> GetAsync<T>(string cacheKey, Func<Task<T?>> factory) where T : class;
    Task RemoveAsync(string cacheKey);
}
