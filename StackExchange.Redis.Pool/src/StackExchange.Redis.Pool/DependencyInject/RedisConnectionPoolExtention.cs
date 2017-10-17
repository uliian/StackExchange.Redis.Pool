using System;
using System.Collections.Generic;
using System.Text;
using CodeProject.ObjectPool;
using Microsoft.Extensions.DependencyInjection;

namespace StackExchange.Redis.Pool.DependencyInject
{
    public static class RedisConnectionPoolExtention
    {
        public static void AddRedisConnectionPool(this IServiceCollection serviceCollection, ConfigurationOptions config, int poolSize)
        {
            serviceCollection.AddSingleton<ObjectPool<PooledConnectionMultiplexer>>(srv =>
                new ObjectPool<PooledConnectionMultiplexer>(poolSize, () => new PooledConnectionMultiplexer(config)));
            serviceCollection.AddScoped<PooledConnectionMultiplexer>(srv => srv.GetRequiredService<ObjectPool<PooledConnectionMultiplexer>>().GetObject());

        }

        public static void AddRedisConnectionPool(this IServiceCollection serviceCollection, string configsStr, int poolSize)
        {
            AddRedisConnectionPool(serviceCollection, ConfigurationOptions.Parse(configsStr), poolSize);
        }
    }
}
