using System;
using System.Threading.Tasks;
using Ecommerce.Data.RepositoryStore;

namespace ECommerce.Cache.RemoteServer.Services
{
    public class RemoteCacheService : IRemoteCacheService
    {
        private readonly IRepositoryStore<CacheItem> _repository;

        public RemoteCacheService(IRepositoryStore<CacheItem> repository)
        {
            _repository = repository;
        }

        public async Task AddItem(string key, string value, TimeSpan? expiredIn = null)
        {
            var cacheItem = new CacheItem(key, value, _repository, expiredIn);
            await _repository.AddAsync(cacheItem);
        }

        public async Task UpdateItem(string key, string newValue, TimeSpan? expiredIn = null)
        {
            var getItemResult = await _repository.SearchASingleItemAsync(x => x.Key == key);

            if (getItemResult != null)
            {
                await _repository.RemoveAsync(getItemResult);
                var cacheItem = new CacheItem(key, newValue, _repository, expiredIn ?? getItemResult.ExpiredIn);
                await _repository.AddAsync(cacheItem);
            }
        }

        public async Task<string> GetItem(string key)
        {
            var getItemResult = await _repository.SearchASingleItemAsync(x => x.Key == key);
            return getItemResult?.Value;
        }

        public async Task RemoveItem(string key)
        {
            var getItemResult = await _repository.SearchASingleItemAsync(x => x.Key == key);
            await _repository.RemoveAsync(getItemResult);
        }
    }
}