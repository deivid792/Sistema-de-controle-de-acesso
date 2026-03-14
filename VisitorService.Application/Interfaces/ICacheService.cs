namespace VisitorService.aplication.Interface.Cache
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string Key);
        Task SetAsync<T>(string Ket, T value, TimeSpan? expiration = null);

        Task RemoveAsync(string Key);
    }
}

