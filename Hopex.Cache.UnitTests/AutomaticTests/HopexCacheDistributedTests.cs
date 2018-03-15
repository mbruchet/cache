using Hopex.Cache.Client;
using System;
using Xunit;
using System.ComponentModel.DataAnnotations;
namespace Hopex.Cache.Client.Tests
{
    public class HopexCacheDistributed_UnitTests
    {
        [Fact]
        public void HopexCacheDistributed_Get_ShouldThrowArgumentNullException()
        {
            Assert.Throws<System.ArgumentNullException>(() =>
            {
                var context = new HopexCacheDistributed(null);
                context.Get(null);
            });
        }
        [Fact]
        public void HopexCacheDistributed_GetAsync_ShouldThrowArgumentNullException()
        {
            Assert.Throws<System.ArgumentNullException>(() =>
            {
                var context = new HopexCacheDistributed(null);
                context.GetAsync(null, new System.Threading.CancellationToken());
            });
        }
        [Fact]
        public void HopexCacheDistributed_Refresh_ShouldThrowArgumentNullException()
        {
            Assert.Throws<System.ArgumentNullException>(() =>
            {
                var context = new HopexCacheDistributed(null);
                context.Refresh(null);
            });
        }
        [Fact]
        public void HopexCacheDistributed_RefreshAsync_ShouldThrowArgumentNullException()
        {
            Assert.Throws<System.ArgumentNullException>(() =>
            {
                var context = new HopexCacheDistributed(null);
                context.RefreshAsync(null, new System.Threading.CancellationToken());
            });
        }
        [Fact]
        public void HopexCacheDistributed_Remove_ShouldThrowArgumentNullException()
        {
            Assert.Throws<System.ArgumentNullException>(() =>
            {
                var context = new HopexCacheDistributed(null);
                context.Remove(null);
            });
        }
        [Fact]
        public void HopexCacheDistributed_RemoveAsync_ShouldThrowArgumentNullException()
        {
            Assert.Throws<System.ArgumentNullException>(() =>
            {
                var context = new HopexCacheDistributed(null);
                context.RemoveAsync(null, new System.Threading.CancellationToken());
            });
        }
        [Fact]
        public void HopexCacheDistributed_Set_ShouldThrowArgumentNullException()
        {
            Assert.Throws<System.ArgumentNullException>(() =>
            {
                var context = new HopexCacheDistributed(null);
                context.Set(null, null, null);
            });
        }
        [Fact]
        public void HopexCacheDistributed_SetAsync_ShouldThrowArgumentNullException()
        {
            Assert.Throws<System.ArgumentNullException>(() =>
            {
                var context = new HopexCacheDistributed(null);
                context.SetAsync(null, null, null, new System.Threading.CancellationToken());
            });
        }
    }
}
