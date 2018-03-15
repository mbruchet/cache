using Hopex.Cache.Client;
using System;
using Xunit;
using System.ComponentModel.DataAnnotations;
namespace Hopex.Cache.Client.Tests
{
    public class HopexCacheServiceFacade_UnitTests
    {
        [Fact]
        public void HopexCacheServiceFacade_GetAsync_ShouldThrowArgumentNullException()
        {
            Assert.Throws<System.ArgumentNullException>(() =>
            {
                var context = new HopexCacheServiceFacade(null);
                context.GetAsync(null);
            });
        }
        [Fact]
        public void HopexCacheServiceFacade_Get_ShouldThrowArgumentNullException()
        {
            Assert.Throws<System.ArgumentNullException>(() =>
            {
                var context = new HopexCacheServiceFacade(null);
                context.Get(null);
            });
        }
        [Fact]
        public void HopexCacheServiceFacade_RefreshAsync_ShouldThrowArgumentNullException()
        {
            Assert.Throws<System.ArgumentNullException>(() =>
            {
                var context = new HopexCacheServiceFacade(null);
                context.RefreshAsync(new System.Threading.CancellationToken());
            });
        }
        [Fact]
        public void HopexCacheServiceFacade_Refresh_ShouldThrowArgumentNullException()
        {
            Assert.Throws<System.ArgumentNullException>(() =>
            {
                var context = new HopexCacheServiceFacade(null);
                context.Refresh();
            });
        }
        [Fact]
        public void HopexCacheServiceFacade_RemoveAsync_ShouldThrowArgumentNullException()
        {
            Assert.Throws<System.ArgumentNullException>(() =>
            {
                var context = new HopexCacheServiceFacade(null);
                context.RemoveAsync(null, new System.Threading.CancellationToken());
            });
        }
        [Fact]
        public void HopexCacheServiceFacade_Remove_ShouldThrowArgumentNullException()
        {
            Assert.Throws<System.ArgumentNullException>(() =>
            {
                var context = new HopexCacheServiceFacade(null);
                context.Remove(null);
            });
        }
    }
}
