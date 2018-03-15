using Hopex.Cache.Client;
using System;
using Xunit;
using System.ComponentModel.DataAnnotations;
namespace Hopex.Cache.Client.Tests
{
    public class HopexCacheClientRegister_UnitTests
    {
        [Fact]
        public void HopexCacheClientRegister_Register_ShouldThrowArgumentNullException()
        {
            Assert.Throws<System.ArgumentNullException>(() =>
            {
                var context = new HopexCacheClientRegister();
                context.Register(null, null, null);
            });
        }
    }
}
