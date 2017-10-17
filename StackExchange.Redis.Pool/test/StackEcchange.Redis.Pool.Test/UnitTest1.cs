using System;
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
            collection.AddRedisConnectionPool("localhost:6388", 10);
            provider = collection.BuildServiceProvider();
            
        }

        [Fact]
        public void Test1()
        {
            using (var conn = provider.GetService<PooledConnectionMultiplexer>())
            {
                var db = conn.ConnectionMultiplexer.GetDatabase();
                db.StringSet("test", "test");
                var value = db.StringGet("test");
                Assert.True(value.HasValue);
                Assert.Equal(value.ToString(),"test");
            }
            
        }
    }
}
