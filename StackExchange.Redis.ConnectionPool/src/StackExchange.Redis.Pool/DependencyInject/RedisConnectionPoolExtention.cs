using System;
using CodeProject.ObjectPool;
using Microsoft.Extensions.DependencyInjection;

namespace StackExchange.Redis.ConnectionPool.DependencyInject
{
    public static class RedisConnectionPoolExtention
    {
        public static void AddRedisConnectionPool(this IServiceCollection serviceCollection, ConfigurationOptions config, int poolSize)
        {
            serviceCollection.AddSingleton<ObjectPool<PooledConnectionMultiplexer>>(srv =>
                new ObjectPool<PooledConnectionMultiplexer>(poolSize, () => new PooledConnectionMultiplexer(config)));
            serviceCollection.AddScoped<IConnectionMultiplexer>(srv => srv.GetRequiredService<ObjectPool<PooledConnectionMultiplexer>>().GetObject());
        }

        public static void AddRedisConnectionPool(this IServiceCollection serviceCollection, Action<ConfigurationOptions> configAction, int poolSize)
        {
            var config = new ConfigurationOptions();
            configAction(config);
            AddRedisConnectionPool(serviceCollection, config, poolSize);
        }

        public static void AddRedisConnectionPool(this IServiceCollection serviceCollection, string configsStr, int poolSize)
        {
            AddRedisConnectionPool(serviceCollection, ConfigurationOptions.Parse(configsStr), poolSize);
        }
    }
}
