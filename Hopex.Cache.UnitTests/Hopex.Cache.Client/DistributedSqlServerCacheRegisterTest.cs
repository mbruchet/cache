using Hopex.Cache.Client;
using Microsoft.Extensions.Caching.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Debug;
using Newtonsoft.Json;
using Xunit;

namespace Hopex.Cache.UnitTests.Hopex.Cache.Client
{
    public class DistributedSqlServerCacheRegisterTest
    {
        [Fact]
        public void Should_Register()
        {
            var register = new DistributedSqlServerCacheRegister();
            register.Register(new CacheServiceOptions {CacheParameter = JsonConvert.SerializeObject(new SqlServerCacheOptions()) },new DebugLogger("test"), new ServiceCollection() );
        }
    }
}
