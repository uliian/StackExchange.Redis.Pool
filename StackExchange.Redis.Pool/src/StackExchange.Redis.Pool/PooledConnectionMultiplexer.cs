using System;
using System.Collections.Generic;
using System.Text;
using CodeProject.ObjectPool;

namespace StackExchange.Redis.Pool
{
    public class PooledConnectionMultiplexer : PooledObject
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        public PooledConnectionMultiplexer(ConfigurationOptions config)
        {
            this._connectionMultiplexer = ConnectionMultiplexer.Connect(config);
            this.OnValidateObject += context => this._connectionMultiplexer.IsConnected;
        }

        public PooledConnectionMultiplexer(string configStr):this(ConfigurationOptions.Parse(configStr))
        {
        }

        public ConnectionMultiplexer ConnectionMultiplexer => this._connectionMultiplexer;
    }
}
