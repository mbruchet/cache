using Hopex.Cache.Client;
using System;
using Xunit;
using System.ComponentModel.DataAnnotations;
namespace Hopex.Cache.Client.Tests
{
    public class DistributedRedisCacheRegister_UnitTests
    {
        [Fact]
        public void DistributedRedisCacheRegister_Register_ShouldThrowArgumentNullException()
        {
            Assert.Throws<System.ArgumentNullException>(() =>
            {
                var context = new DistributedRedisCacheRegister();
                context.Register(null, null, null);
            });
        }
    }
}
