//------------------------------------------------------------
// <copyright file="DistributedSqlServerCacheRegister.cs" company="Mega">
//    Copyright (c) 2017 Mega
// </copyright>
// <summary>
// Register a sql server cache service
// </summary>
//------------------------------------------------------------

using System;
using Hopex.Quality.CodeQuality;
using Microsoft.Extensions.Caching.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Hopex.Cache.Client
{
    public class DistributedSqlServerCacheRegister:ICacheClientRegister
    {
        public void Register(CacheServiceOptions cacheConfiguration, ILogger logger, IServiceCollection services)
        {
            this.ValidateMethod(cacheConfiguration, logger, services);

            logger.LogInformation(AppDomain.CurrentDomain.Id, $"CacheService settings cnxString:{cacheConfiguration.CacheParameter}");

            var cacheParameter =
                JsonConvert.DeserializeObject<SqlServerCacheOptions>(cacheConfiguration.CacheParameter);

            if (cacheParameter == null)
                return;

            services.AddDistributedSqlServerCache(sql =>
            {
                sql.ConnectionString = cacheParameter.ConnectionString;
                sql.DefaultSlidingExpiration = cacheParameter.DefaultSlidingExpiration;
                sql.ExpiredItemsDeletionInterval = cacheParameter.ExpiredItemsDeletionInterval;
                sql.SchemaName = cacheParameter.SchemaName;
                sql.SystemClock = cacheParameter.SystemClock;
                sql.TableName = cacheParameter.TableName;
            });

        }
    }
}
