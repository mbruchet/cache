using System;
using System.Collections.Concurrent;
using System.Threading;

namespace ECommerce.Cache.Client
{
    internal class CacheItem
    {
        public string Key { get; set; }
        public object Item { get; set; }

        private readonly TimeSpan? _expiresInTime;
        private readonly ConcurrentDictionary<string, CacheItem> _items;

        private readonly Timer _timer;
        private Func<object> _reloadFunc;

        public CacheItem(string key, object item, ConcurrentDictionary<string, CacheItem> items, TimeSpan? expiresInTime, Func<object> reloadFunc)
        {
            Key = key;
            Item = item;
            _expiresInTime = expiresInTime;
            _items = items;
            _reloadFunc = reloadFunc;

            if (expiresInTime.HasValue)
            {
                _timer = new Timer(CallBack, null, expiresInTime.Value, Timeout.InfiniteTimeSpan);
            }
        }

        private void CallBack(object source)
        {
            _items.TryRemove(Key, out var item);

            if (_reloadFunc == null) return;

            var newItem = _reloadFunc();
            _items.TryAdd(Key, new CacheItem(Key, newItem, _items, _expiresInTime, _reloadFunc));
        }
    }
}