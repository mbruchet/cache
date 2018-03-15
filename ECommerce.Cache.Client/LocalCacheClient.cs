using System;
using System.Collections.Concurrent;

namespace ECommerce.Cache.Client
{
    public class LocalCacheClient : ICacheClient
    {
        private readonly ConcurrentDictionary<string, CacheItem> _ItemsObjects = new ConcurrentDictionary<string, CacheItem>();

        public T Get<T>(string key) where T:class, new()
        {
            if(_ItemsObjects.ContainsKey(key))
                return (T)_ItemsObjects[key].Item;

            return null;
        }

        public T GetOrInsert<T>(string key, Func<T> func, TimeSpan? expiresInTime = null, Func<T> reloadFunc = null) where T:class, new()
        {
            if (!_ItemsObjects.ContainsKey(key))
            {
                var item = func();                
                _ItemsObjects.TryAdd(key, new CacheItem(key, item, _ItemsObjects, expiresInTime, reloadFunc));
            }

            return (T)_ItemsObjects[key].Item;
        }

        public T UpdateOrInsert<T>(string key, T value, TimeSpan? expiresInTime = null, Func<T> reloadFunc = null) where T : class, new()
        {
            _ItemsObjects.AddOrUpdate(key, new CacheItem(key, value, _ItemsObjects, expiresInTime, reloadFunc),
                (oldKey, old) =>
                {
                    old.Item = value;
                    return old;
                });

            return (T)_ItemsObjects[key].Item;
        }
    }
}
