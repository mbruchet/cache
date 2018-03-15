//---------------------------------------------------------------
// <copyright file="HopexCacheDistributed.cs" company="Mega">
//    Copyright (c) 2017 Mega
// </copyright>
// <summary>
// Class to implement the interface IDistributedCache
// </summary>
//---------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using Hopex.Quality.CodeQuality;
using Hopex.Quality.CodeQuality.Annotations;
using Hopex.Quality.CodeQuality.Attributes;
using Hopex.Quality.CodeQuality.Services;
using Microsoft.Extensions.Caching.Distributed;

namespace Hopex.Cache.Client
{
    [HopexAop]
    public class HopexCacheDistributed : ContextBoundObject, IDistributedCache
    {
        private readonly HopexCacheServiceFacade _facade;

        public HopexCacheDistributed(CacheServiceOptions cacheServiceOptions)
        {
            this.ValidateMethod(cacheServiceOptions);

            cacheServiceOptions.Environment = cacheServiceOptions.Environment ?? "Sys";
            cacheServiceOptions.Language = cacheServiceOptions.Language ??
                                           Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            _facade = new HopexCacheServiceFacade(cacheServiceOptions);
        }

        [RequiredValidator]
        public byte[] Get([ValidationRule(IsRequired =true)]string key)
        {
            return GetAsync(key).Result;
        }

        [RequiredValidator]
        public async Task<byte[]> GetAsync([ValidationRule(IsRequired = true)]string key, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _facade.GetAsync(key);
        }

        [RequiredValidator]
        public void Refresh([ValidationRule(IsRequired = true)]string key)
        {
            RefreshAsync(key).Wait();
        }

        [RequiredValidator]
        public async Task RefreshAsync([ValidationRule(IsRequired = true)]string key, CancellationToken token = default(CancellationToken))
        {
            await _facade.RefreshAsync(token);
        }

        [RequiredValidator]
        public void Remove([ValidationRule(IsRequired =true)]string key)
        {
            RemoveAsync(key).Wait();
        }

        [RequiredValidator]
        public async Task RemoveAsync([ValidationRule(IsRequired =true)]string key, CancellationToken token = default(CancellationToken))
        {
            await _facade.RemoveAsync(key, token);
        }

        [RequiredValidator]
        public void Set([ValidationRule(IsRequired=true)]string key, [ValidationRule(IsRequired =true)] byte[] value, DistributedCacheEntryOptions options)
        {
            SetAsync(key, value, options).Wait();
        }

        [RequiredValidator]
        public async Task SetAsync([ValidationRule(IsRequired = true)]string key, [ValidationRule(IsRequired = true)]byte[] value, DistributedCacheEntryOptions options,
            CancellationToken token = default(CancellationToken))
        {
            await _facade.SetValueAsync(key, value, options.AbsoluteExpiration, options.AbsoluteExpirationRelativeToNow,
                options.SlidingExpiration, token);
        }
    }
}
