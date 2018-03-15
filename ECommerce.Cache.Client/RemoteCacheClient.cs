using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Text;
using System.Threading;
using ECommerce.Remote;
using Newtonsoft.Json;

namespace ECommerce.Cache.Client
{
    public class RemoteCacheClient : ICacheClient
    {
        private const string RequestUri = "/api/RemoteCache";

        private readonly RemoteServiceSettings _settings;
        private readonly HttpClient _httpClient;
        private readonly ConcurrentDictionary<string, ReloadTimer> _timers = new ConcurrentDictionary<string, ReloadTimer>();

        public RemoteCacheClient(RemoteServiceSettings settings, HttpClient httpClient = null)
        {
            _settings = settings;
            _httpClient = httpClient ?? new HttpClient { BaseAddress = new Uri(settings.Uri) };
        }

        public T Get<T>(string key) where T : class, new()
        {
            var response = _httpClient.GetAsync($"{RequestUri}?key={key}").Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;

            if (!string.IsNullOrEmpty(json))
                return JsonConvert.DeserializeObject<T>(json);

            return default(T);
        }

        public T GetOrInsert<T>(string key, Func<T> func, TimeSpan? expiresInTime = null, Func<T> reloadFunc = null) where T : class, new()
        {
            var current = Get<T>(key);

            if (current != null)
                return current;

            var item = func();

            _httpClient.PostAsync(RequestUri,
                new StringContent(JsonConvert.SerializeObject(new
                {
                    key,
                    value = JsonConvert.SerializeObject(item),
                    expiredIn = expiresInTime
                }), Encoding.UTF8, "application/json"));

            if(reloadFunc != null && expiresInTime.HasValue)
                _timers.TryAdd(key, new ReloadTimer(reloadFunc, _httpClient, key, expiresInTime.Value));

            return item;
        }

        public T UpdateOrInsert<T>(string key, T value, TimeSpan? expiresInTime = null, Func<T> reloadFunc = null) where T : class, new()
        {
            var current = Get<T>(key);

            if (current != null)
                _httpClient.PutAsync(RequestUri,
                    new StringContent(JsonConvert.SerializeObject(new
                    {
                        key,
                        value = JsonConvert.SerializeObject(value),
                        expiredIn = expiresInTime
                    }), Encoding.UTF8, "application/json"));
            else
                _httpClient.PostAsync(RequestUri,
                    new StringContent(JsonConvert.SerializeObject(new
                    {
                        key,
                        value = JsonConvert.SerializeObject(value),
                        expiredIn = expiresInTime
                    }), Encoding.UTF8, "application/json"));

            if (reloadFunc != null && expiresInTime.HasValue)
                _timers.TryAdd(key, new ReloadTimer(reloadFunc, _httpClient, key, expiresInTime.Value));

            return value;
        }
    }
}
