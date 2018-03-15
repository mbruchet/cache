using System;

namespace ECommerce.Cache.Client
{
    public interface ICacheClient
    {
        T Get<T>(string key) where T : class, new();
        T GetOrInsert<T>(string key, Func<T> func, TimeSpan? expiresInTime = null, Func<T> reloadFunc = null) where T : class, new();
        T UpdateOrInsert<T>(string key, T value, TimeSpan? expiresInTime = null, Func<T> reloadFunc = null) where T : class, new();
    }
}