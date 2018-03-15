using Hopex.Cache.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Xunit;

namespace Hopex.Cache.UnitTests.Hopex.Cache.Client
{

    public class CacheRegisterTests
    {
        [Fact]
        public void ShouldRegisterCacheServices()
        {
            var register = new CacheRegister();
            var services = new ServiceCollection();
            IConfiguration configuration = new ConfigurationBuilder()
                                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                    .AddJsonFile("appSettings.json")
                                    .Build();
            var cacheConfiguration = configuration.GetSection(nameof(Cache));
            register.RegisterCacheService(services, cacheConfiguration, new LoggerFactory());
        }
        [Fact]
        public void ShouldRegister()
        {
            var register = new CacheRegister();
            var services = new ServiceCollection();
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appSettings.json")
                .Build();
            services.AddSingleton(configuration);
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            register.RegisterService(services);
        }
    }
}
