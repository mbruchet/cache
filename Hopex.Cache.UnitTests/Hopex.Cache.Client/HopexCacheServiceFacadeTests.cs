using System;
using FluentAssertions;
using Hopex.Cache.Client;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Hopex.Cache.UnitTests.Hopex.Cache.Client
{
    public class HopexCacheServiceFacadeTests
    {
        [Fact]
        public async  Task Should_getasync_data_returns_null_when_key_doesnt_exist()
        {
            var facade = new HopexCacheServiceFacade(new CacheServiceOptions());
            var key = await facade.GetAsync("key");
            key.Should().BeNull();
        }
        [Fact]
        public async Task Should_getasync_data_returns_value_when_key_exists()
        {
            var facade = new HopexCacheServiceFacade(new CacheServiceOptions());
            await facade.SetValueAsync("key", Encoding.UTF8.GetBytes("test"));
            var key = facade.Get("key");
            key.Should().NotBeNull();
            key.Should().Equal(Encoding.UTF8.GetBytes("test"));
        }

        [Fact]
        public void Should_get_data_returns_null_when_key_doesnt_exist()
        {
            var facade = new HopexCacheServiceFacade(new CacheServiceOptions());
            var key = facade.Get("key");
            key.Should().BeNull();
        }
        [Fact]
        public async Task Should_get_data_returns_value_when_key_exists()
        {
            var facade = new HopexCacheServiceFacade(new CacheServiceOptions());
            await facade.SetValueAsync("key", Encoding.UTF8.GetBytes("test"));
            var key = facade.Get("key");
            key.Should().NotBeNull();
            key.Should().Equal(Encoding.UTF8.GetBytes("test"));
        }
        [Fact]
        public async Task Should_Refresh_async_data()
        {
            var facade = new HopexCacheServiceFacade(new CacheServiceOptions());
            await facade.RefreshAsync(CancellationToken.None);
        }

        [Fact]
        public void Should_Refresh_data()
        {
            var facade = new HopexCacheServiceFacade(new CacheServiceOptions());
             facade.Refresh();
        }

        [Fact]
        public void Should_Remove()
        {
            var facade = new HopexCacheServiceFacade(new CacheServiceOptions());
            facade.Remove("key");
        }

        [Fact]
        public async Task Should_Remove_async()
        {
            var facade = new HopexCacheServiceFacade(new CacheServiceOptions());
           await facade.RemoveAsync("key",CancellationToken.None);
        }

        [Fact]
        public async Task Should_SetValue()
        {
            var facade = new HopexCacheServiceFacade(new CacheServiceOptions());
            await facade.SetValueAsync("key", Encoding.UTF8.GetBytes("test"), DateTimeOffset.MaxValue,
                TimeSpan.FromDays(30),TimeSpan.FromDays(30),CancellationToken.None);
        }
    }
}
