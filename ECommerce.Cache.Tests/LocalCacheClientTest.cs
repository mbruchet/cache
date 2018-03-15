using System;
using System.Threading.Tasks;
using ECommerce.Cache.Client;
using Xunit;

namespace ECommerce.Cache.Tests
{
    public class LocalCacheClientTest
    {
        LocalCacheClient _cacheClient = new LocalCacheClient();

        [Fact]
        public void ShouldReturnNullIfItemNotExists()
        {
            var data = _cacheClient.Get<DocumentTest>("MyTest");
            Assert.Null(data);
        }

        [Fact]
        public void WhenItemDoesnotExistsShouldAddItThenReturnItem()
        {
            var data = _cacheClient.GetOrInsert("MyTest",
                () => new DocumentTest {Key = Guid.NewGuid().ToString(), Title = "MyDocument"});

            Assert.NotNull(data);
        }

        [Fact]
        public void WhemItemExistsReplaceItElseAddItemThenReturnItem()
        {
            var data = _cacheClient.GetOrInsert("MyTest",
                () => new DocumentTest {Key = Guid.NewGuid().ToString(), Title = "MyTest" });

            Assert.NotNull(data);

            Assert.Equal("MyTest", data.Title);

            data.Title += "Mod";

            data = _cacheClient.UpdateOrInsert("MyTest", data, TimeSpan.FromMilliseconds(1000));

            Assert.NotNull(data);
            Assert.Equal("MyTestMod", data.Title);

            data = _cacheClient.UpdateOrInsert<DocumentTest>("MyTest2",
                new DocumentTest {Key = Guid.NewGuid().ToString(), Title = "MyTest2"}, TimeSpan.FromMilliseconds(1000));

            Assert.NotNull(data);
            Assert.Equal("MyTest2", data.Title);
        }

        [Fact]
        public void WhenAnItemAddedToACacheHasExpiredThenItShouldNotReturned()
        {
            var data = _cacheClient.GetOrInsert("MyTest",
                () => new DocumentTest { Key = Guid.NewGuid().ToString(), Title = "MyTest" }, TimeSpan.FromMilliseconds(1000));

            Assert.NotNull(data);

            Assert.Equal("MyTest", data.Title);

            Task.Delay(1000).Wait();

            data = _cacheClient.Get<DocumentTest>("MyTest");

            Assert.Null(data);
        }

        [Fact]
        public void WhenAnItemAddedtoAcacheExpiredThenAcallBackFunctionShouldBeCalled()
        {
            var data = _cacheClient.GetOrInsert("MyTest",
                () => new DocumentTest { Key = Guid.NewGuid().ToString(), Title = "MyTest" }, 
                TimeSpan.FromMilliseconds(1000), () => new DocumentTest { Key = Guid.NewGuid().ToString(), Title = "MyTestReload" });

            Assert.NotNull(data);

            Assert.Equal("MyTest", data.Title);

            data = _cacheClient.Get<DocumentTest>("MyTest");

            while (data == null)
            {
                data = _cacheClient.Get<DocumentTest>("MyTest");
                Task.Delay(100).Wait();
            }

            Assert.NotNull(data);
        }
    }
}
