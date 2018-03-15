using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace ECommerce.Cache.Client
{
    public class ReloadTimer
    {
        private readonly Func<object> _reloadFunc;
        private readonly HttpClient _httpClient;
        private readonly string _key;
        private readonly TimeSpan _expiresInTime;
        private Timer _timer;
        private const string RequestUri = "/api/RemoteCache";

        public ReloadTimer(Func<object> reloadFunc, HttpClient httpClient, string key, TimeSpan expiresInTime)
        {
            _reloadFunc = reloadFunc;
            _httpClient = httpClient;
            _key = key;
            _expiresInTime = expiresInTime;
            _timer = new Timer(CallBack, null, expiresInTime, Timeout.InfiniteTimeSpan);
        }

        private void CallBack(object source)
        {
            var item = _reloadFunc?.Invoke();

            _httpClient.PostAsync(RequestUri,
                new StringContent(JsonConvert.SerializeObject(new
                {
                    _key,
                    value = JsonConvert.SerializeObject(item),
                    expiredIn = _expiresInTime
                }), Encoding.UTF8, "application/json"));
        }
    }
}