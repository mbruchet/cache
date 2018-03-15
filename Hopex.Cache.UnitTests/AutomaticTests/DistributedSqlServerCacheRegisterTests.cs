using Hopex.Cache.Client;
using System;
using Xunit;
using System.ComponentModel.DataAnnotations;
namespace Hopex.Cache.Client.Tests
{
    public class DistributedSqlServerCacheRegister_UnitTests
    {
        [Fact]
        public void DistributedSqlServerCacheRegister_Register_ShouldThrowArgumentNullException()
        {
            Assert.Throws<System.ArgumentNullException>(() =>
            {
                var context = new DistributedSqlServerCacheRegister();
                context.Register(null, null, null);
            });
        }
    }
}
