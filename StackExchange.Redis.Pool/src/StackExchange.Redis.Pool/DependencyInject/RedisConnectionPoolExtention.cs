using System;
using System.Collections.Generic;
using System.Text;
using CodeProject.ObjectPool;

namespace StackExchange.Redis.Pool.DependencyInject
{
    public static class RedisConnectionPoolExtention
    {
        public static void AddRedisConnectionPool(ConfigurationOptions config,int poolSize)
        {
            new ObjectPool<PooledConnectionMultiplexer>(poolSize, () => new PooledConnectionMultiplexer(config));
        }
    }
}
