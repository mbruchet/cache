using Hopex.Cache.Client;
using System;
using Xunit;
using System.ComponentModel.DataAnnotations;
namespace Hopex.Cache.Client.Tests
{
    public class CacheRegister_UnitTests
    {
        [Fact]
        public void CacheRegister_RegisterCacheService_ShouldThrowArgumentNullException()
        {
            Assert.Throws<System.ArgumentNullException>(() =>
            {
                var context = new CacheRegister();
                context.RegisterCacheService(null, null, null);
            });
        }
        [Fact]
        public void CacheRegister_RegisterService_ShouldThrowArgumentNullException()
        {
            Assert.Throws<System.ArgumentNullException>(() =>
            {
                var context = new CacheRegister();
                context.RegisterService(null);
            });
        }


    }
}
