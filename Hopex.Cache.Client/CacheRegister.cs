//------------------------------------------------------------
// <copyright file="CacheRegister.cs" company="Mega">
//    Copyright (c) 2017 Mega
// </copyright>
// <summary>
// Registres the cache service
// </summary>
//------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Hopex.Core.Contracts.Interfaces;
using Hopex.Quality.CodeQuality;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Hopex.Cache.Client
{
    public class CacheRegister : ICacheRegister
    {
        public void RegisterCacheService(IServiceCollection services, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            this.ValidateMethod(services, configuration, loggerFactory);

            var cacheServiceConfiguration = new CacheServiceOptions();

            configuration.Bind(cacheServiceConfiguration);
            services.AddSingleton(cacheServiceConfiguration);

            var typeName = cacheServiceConfiguration.CacheTypeName;

            var logger = loggerFactory.CreateLogger("Startup");
            logger.LogInformation(AppDomain.CurrentDomain.Id, $"CacheService settings type:{typeName}");
            logger.LogInformation(AppDomain.CurrentDomain.Id, $"CacheService settings Parameter:{typeName}");

            var type = GetType(typeName);

            var register = (ICacheClientRegister)Activator.CreateInstance(type ?? throw new InvalidOperationException());
            register.Register(cacheServiceConfiguration, logger, services);
        }

        private static Type GetType(string typeName)
        {
            return Type.GetType(typeName, (name) =>
            {
                var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.FullName == name.FullName);

                if (assembly != null)
                    return assembly;

                var directoryInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

                var filePath = Path.Combine(directoryInfo.FullName, name.FullName + ".dll");

                if (File.Exists(filePath))
                {
                    assembly = Assembly.LoadFrom(filePath);
                    return assembly;
                }

                assembly = Assembly.LoadFile(name.FullName);
                return assembly;

            }, null, true);
        }

        public void RegisterService(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            var configuration = serviceProvider.GetService<IConfiguration>();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            var configSection = configuration?.GetSection(nameof(Cache));

            RegisterCacheService(services, configSection, loggerFactory);
        }
    }
}
