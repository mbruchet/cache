using System;
using System.Threading;
using Ecommerce.Data.RepositoryStore;

namespace ECommerce.Cache.RemoteServer.Services
{
    public class CacheItem
    {
        private IRepositoryStore<CacheItem> _repository;
        private Timer _timer;

        public CacheItem() { }

        public CacheItem(string key, string value, IRepositoryStore<CacheItem> repository, TimeSpan? expiredin)
        {
            Key = key;
            Value = value;
            _repository = repository;
            ExpiredIn = expiredin;

            if (expiredin.HasValue)
            {
                _timer = new Timer(CallBack, null, expiredin.Value, Timeout.InfiniteTimeSpan);
            }
        }

        private void CallBack(object source)
        {
            _repository.RemoveAsync(this).Wait();
        }

        public string Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public TimeSpan? ExpiredIn { get; set; }
    }
}