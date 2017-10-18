using System;
using System.Linq;
using System.Threading.Tasks;
using CodeProject.ObjectPool;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using StackExchange.Redis.Pool;
using StackExchange.Redis.Pool.DependencyInject;
using Xunit;

namespace StackEcchange.Redis.Pool.Test
{
    public class PooledConnectionMultiplexerTest
    {
        private IServiceProvider provider;


        public PooledConnectionMultiplexerTest()
        {
            IServiceCollection collection = new ServiceCollection();
            collection.AddRedisConnectionPool("localhost:6379", 10);
            provider = collection.BuildServiceProvider();

        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]

        public void SyncTest(int x)
        {
            var conn = provider.GetService<IConnectionMultiplexer>();

            var db = conn.GetDatabase();
            db.StringSet("test", "test" + x);
            var value = db.StringGet("test");
            Assert.True(value.HasValue);
            Assert.Equal(value.ToString(), "test" + x);
        }

        [Fact]

        public void ParallelTest()
        {
            void RunTest(int x)
            {
                var conn = provider.GetService<IConnectionMultiplexer>();

                var db = conn.GetDatabase();
                db.StringSet("test" + x, "test" + x);
                var value = db.StringGet("test" + x);
                Assert.True(value.HasValue);
                Assert.Equal(value.ToString(), "test" + x);
            }

            Enumerable.Range(0, 1000).AsParallel().WithDegreeOfParallelism(10).ForAll(RunTest);
        }

        [Fact]

        public void ParallelTest2()
        {
            var pool = this.provider.GetService<ObjectPool<PooledConnectionMultiplexer>>();
            void RunTest(int x)
            {
                using (var conn = pool.GetObject())
                {
                    var db = conn.GetDatabase();
                    db.StringSet("test" + x, "test" + x);
                    var value = db.StringGet("test" + x);
                    Assert.True(value.HasValue);
                    Assert.Equal(value.ToString(), "test" + x);
                }
                
            }
            Enumerable.Range(0, 1000).AsParallel().WithDegreeOfParallelism(10).ForAll(RunTest);
        }
    }
}
