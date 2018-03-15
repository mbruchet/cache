//---------------------------------------------------------------
// <copyright file="HopexCacheClientRegister.cs" company="Mega">
//    Copyright (c) 2017 Mega
// </copyright>
// <summary>
// Class to register Hopex Cache client
// </summary>
//---------------------------------------------------------------

using Hopex.Quality.CodeQuality;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Hopex.Cache.Client
{
    public class HopexCacheClientRegister: ICacheClientRegister
    {
        public void Register(CacheServiceOptions cacheConfiguration, ILogger logger, IServiceCollection services)
        {
            this.ValidateMethod(cacheConfiguration, logger, services);
            var hopexCacheDistributed = new HopexCacheDistributed(cacheConfiguration);
            services.AddSingleton<IDistributedCache>(hopexCacheDistributed);
        }
    }
}
