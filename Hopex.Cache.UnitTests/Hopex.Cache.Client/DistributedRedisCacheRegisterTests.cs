using Hopex.Cache.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Debug;
using Xunit;

namespace Hopex.Cache.UnitTests.Hopex.Cache.Client
{
   public  class DistributedRedisCacheRegisterTests
    {
        [Fact]
        public void Should_Register()
        {
            var cacheRegister = new DistributedRedisCacheRegister();
            cacheRegister.Register(new CacheServiceOptions(), new DebugLogger("test"),new ServiceCollection() );
        }
    }
}
