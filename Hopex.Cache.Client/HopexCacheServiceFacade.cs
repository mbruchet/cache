//---------------------------------------------------------------
// <copyright file="HopexCacheServiceFacade.cs" company="Mega">
//    Copyright (c) 2017 Mega
// </copyright>
// <summary>
// Facade will be used in future HopexCacheServiceFacade
// </summary>
//---------------------------------------------------------------
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;
using System.Threading.Tasks;
using Hopex.Quality.CodeQuality;
using Hopex.Quality.CodeQuality.Annotations;
using Hopex.Quality.CodeQuality.Attributes;
using Hopex.Quality.CodeQuality.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Hopex.Cache.Client
{
    [HopexAop]
    public class HopexCacheServiceFacade : ContextBoundObject
    {
        [RequiredFieldValidator]
        public CacheServiceOptions ServiceOptions { get; }

        private readonly MemoryCache _cache;

        public HopexCacheServiceFacade(CacheServiceOptions cacheServiceOptions)
        {
            this.ValidateMethod(cacheServiceOptions);

            ServiceOptions = cacheServiceOptions;

            _cache = new MemoryCache(new MemoryCacheOptions { CompactionPercentage = 0.3 });

            ModelValidatorExtension.IsRequired(_cache, "MemoryCache");
        }

        [RequiredValidator]
        public async Task<byte[]> GetAsync([ValidationRule(IsRequired = true)]string key)
        {
            return await Task.FromResult(Get(key));
        }

        [RequiredValidator]
        public byte[] Get([ValidationRule(IsRequired = true)]string key)
        {
            return _cache.TryGetValue(key, out var obj) ? (byte[])obj : null;
        }

        [RequiredValidator]
        public async Task RefreshAsync([ValidationRule(IsRequired = true)]CancellationToken cancellationToken)
        {
            await Task.Run(() => Refresh(), cancellationToken);
        }

        public void Refresh()
        {
            _cache.Compact(100);
        }

        [RequiredValidator]
        public async Task RemoveAsync([ValidationRule(IsRequired = true)]string key, CancellationToken cancellationToken)
        {
            await Task.Run(() => Remove(key), cancellationToken);
        }

        [RequiredValidator]
        public void Remove([ValidationRule(IsRequired = true)]string key)
        {
            _cache.Remove(key);
        }

        [RequiredValidator]
        public async Task SetValueAsync([ValidationRule(IsRequired = true)]string key, byte[] value, DateTimeOffset? optionsAbsoluteExpiration = null, TimeSpan? optionsAbsoluteExpirationRelativeToNow = null, TimeSpan? optionsSlidingExpiration = null, CancellationToken token = default(CancellationToken))
        {
            await Task.Run(() => SetValue(key, value, optionsAbsoluteExpiration, optionsAbsoluteExpirationRelativeToNow, optionsSlidingExpiration), token);
        }

        [RequiredValidator]
        private void SetValue([ValidationRule(IsRequired = true)]string key, [ValidationRule(IsRequired = true)]byte[] value, DateTimeOffset? optionsAbsoluteExpiration, TimeSpan? optionsAbsoluteExpirationRelativeToNow, TimeSpan? optionsSlidingExpiration)
        {
            if (optionsAbsoluteExpiration != null) _cache.Set(key, value, optionsAbsoluteExpiration.Value);
            else if (optionsAbsoluteExpirationRelativeToNow != null) _cache.Set(key, value, DateTimeOffset.Now.Add(optionsAbsoluteExpirationRelativeToNow.Value));
            else if (optionsSlidingExpiration != null) _cache.Set(key, value, optionsSlidingExpiration.Value);
            else _cache.Set(key, value);
        }
    }
}
