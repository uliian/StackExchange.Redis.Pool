# StackExchange.Redis.Pool

For StackExchange.Redis connection pool

## Build Status

[![Build Status](https://travis-ci.org/uliian/StackExchange.Redis.Pool.svg?branch=master)](https://travis-ci.org/uliian/StackExchange.Redis.Pool)

## How to use

### Install

``` Powershell
Install-Package StackExchange.Redis.ConnectionPool
```

### Add Dependency Inject

add dependency inject to `IServiceCollection`

``` Csharp
collection.AddRedisConnectionPool("localhost:6379", 10);
```

OR

``` Csharp
collection.AddRedisConnectionPool(option=>{
    // todo config
}, 10);
```

the more config infomation,look at StackEchange.Redis

doc:<https://stackexchange.github.io/StackExchange.Redis/Configuration>

if you need one connection object per scope(like one user one request)

direct use inject object: `IConnectionMultiplexer`

if you need pool (for Parallel case)

use pool object : `ObjectPool<PooledConnectionMultiplexer>`