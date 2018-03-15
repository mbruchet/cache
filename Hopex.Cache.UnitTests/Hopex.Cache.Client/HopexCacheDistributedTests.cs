using FluentAssertions;
using Hopex.Cache.Client;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Hopex.Cache.UnitTests.Hopex.Cache.Client
{
    public  class HopexCacheDistributedTests
    {
        [Fact]
        public void Should_Returns_null_When_key_doesnt_exist()
        {
            var  cacheDistributed = new HopexCacheDistributed(new CacheServiceOptions());
            var result = cacheDistributed.Get("key");
            result.Should().BeNull();
        }

        [Fact]
        public async Task Should_Returns_data_When_key_exists()
        {
            var cacheDistributed = new HopexCacheDistributed(new CacheServiceOptions());
            await cacheDistributed.SetAsync("key", Encoding.UTF8.GetBytes("test"), new DistributedCacheEntryOptions(), CancellationToken.None);
            var result = cacheDistributed.Get("key");
        }

        [Fact]
        public void Should_set()
        {
            var cacheDistributed = new HopexCacheDistributed(new CacheServiceOptions());
            cacheDistributed.Set("key",Encoding.UTF8.GetBytes("test"));
        }
        [Fact]
        public async Task Should_set_async()
        {
            var cacheDistributed = new HopexCacheDistributed(new CacheServiceOptions());
            await cacheDistributed.SetAsync("key", Encoding.UTF8.GetBytes("test"));
        }
        [Fact]
        public void Should_Refresh()
        {
            var cacheDistributed = new HopexCacheDistributed(new CacheServiceOptions());
            cacheDistributed.Refresh("key");
        }
        [Fact]
        public async Task Should_Refresh_async()
        {
            var cacheDistributed = new HopexCacheDistributed(new CacheServiceOptions());
            await cacheDistributed.RefreshAsync("key");
        }

        [Fact]
        public async Task Should_Remove_async()
        {
            var cacheDistributed = new HopexCacheDistributed(new CacheServiceOptions());
            await cacheDistributed.RemoveAsync("key");
        }

        [Fact]
        public void Should_Remove()
        {
            var cacheDistributed = new HopexCacheDistributed(new CacheServiceOptions());
             cacheDistributed.Remove("key");
        }
    }
}
