//------------------------------------------------------------
// <copyright file="DistributedRedisCacheRegister.cs" company="Mega">
//    Copyright (c) 2017 Mega
// </copyright>
// <summary>
// Register a redis cache service
// </summary>
//------------------------------------------------------------

using System;
using Hopex.Quality.CodeQuality;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Hopex.Cache.Client
{
    public class DistributedRedisCacheRegister:ICacheClientRegister
    {
        public void Register(CacheServiceOptions cacheConfiguration, ILogger logger, IServiceCollection services)
        {
            this.ValidateMethod(cacheConfiguration, logger, services);

            logger.LogInformation(AppDomain.CurrentDomain.Id, $"CacheService settings cnxString:{cacheConfiguration.CacheParameter}");

            services.AddDistributedRedisCache(redis => { redis.Configuration = cacheConfiguration.CacheParameter; });
        }
    }
}
