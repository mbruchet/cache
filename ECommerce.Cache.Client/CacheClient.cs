using System;
using System.Net.Http;
using ECommerce.Remote;

namespace ECommerce.Cache.Client
{
    public class CacheClient:ICacheClient
    {
        private readonly ICacheClient _cacheClient;

        public CacheClient(RemoteServiceSettings remoteServiceSettings, HttpClient httpClient = null)
        {
            if(remoteServiceSettings.IsLocal)
                _cacheClient = new LocalCacheClient();
            else
                _cacheClient = new RemoteCacheClient(remoteServiceSettings, httpClient);
        }

        public T Get<T>(string key) where T : class, new()
        {
            return _cacheClient.Get<T>(key);
        }

        public T GetOrInsert<T>(string key, Func<T> func, TimeSpan? expiresInTime = null, Func<T> reloadFunc = null) where T : class, new()
        {
            return _cacheClient.GetOrInsert(key, func, expiresInTime, reloadFunc);
        }

        public T UpdateOrInsert<T>(string key, T value, TimeSpan? expiresInTime = null, Func<T> reloadFunc = null) where T : class, new()
        {
            return _cacheClient.UpdateOrInsert(key, value, expiresInTime, reloadFunc);
        }
    }
}
