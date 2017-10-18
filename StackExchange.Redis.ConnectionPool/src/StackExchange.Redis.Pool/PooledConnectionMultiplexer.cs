// Copyright (c) ULiian. All rights reserved.

using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using CodeProject.ObjectPool;

namespace StackExchange.Redis.ConnectionPool
{
    public class PooledConnectionMultiplexer : PooledObject, IConnectionMultiplexer
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;

        public PooledConnectionMultiplexer(ConfigurationOptions config)
        {
            this._connectionMultiplexer = ConnectionMultiplexer.Connect(config);
            this.OnValidateObject += context => this._connectionMultiplexer.IsConnected;
        }

        public PooledConnectionMultiplexer(string configStr)
            : this(ConfigurationOptions.Parse(configStr))
        {
        }

        /// <summary>Indicates whether any servers are connected</summary>
        public bool IsConnecte => this._connectionMultiplexer.IsConnected;

        //public ConnectionMultiplexer ConnectionMultiplexer => this._connectionMultiplexer;

        /// <summary>
        ///     Sets an IProfiler instance for this ConnectionMultiplexer.
        ///     An IProfiler instances is used to determine which context to associate an
        ///     IProfiledCommand with.  See BeginProfiling(object) and FinishProfiling(object)
        ///     for more details.
        /// </summary>
        public void RegisterProfiler(IProfiler profiler)
        {
            this._connectionMultiplexer.RegisterProfiler(profiler);
        }

        /// <summary>
        ///     Begins profiling for the given context.
        ///     If the same context object is returned by the registered IProfiler, the IProfiledCommands
        ///     will be associated with each other.
        ///     Call FinishProfiling with the same context to get the assocated commands.
        ///     Note that forContext cannot be a WeakReference or a WeakReference&lt;T&gt;
        /// </summary>
        public void BeginProfiling(object forContext)
        {
            this._connectionMultiplexer.BeginProfiling(forContext);
        }

        /// <summary>
        ///     Stops profiling for the given context, returns all IProfiledCommands associated.
        ///     By default this may do a sweep for dead profiling contexts, you can disable this by passing "allowCleanupSweep:
        ///     false".
        /// </summary>
        public ProfiledCommandEnumerable FinishProfiling(object forContext, bool allowCleanupSweep = true)
        {
            return this._connectionMultiplexer.FinishProfiling(forContext, allowCleanupSweep);
        }

        /// <summary>Get summary statistics associates with this server</summary>
        public ServerCounters GetCounters()
        {
            return this._connectionMultiplexer.GetCounters();
        }

        /// <summary>Gets all endpoints defined on the server</summary>
        /// <returns></returns>
        public EndPoint[] GetEndPoints(bool configuredOnly = false)
        {
            return this._connectionMultiplexer.GetEndPoints(configuredOnly);
        }

        /// <summary>
        ///     Wait for a given asynchronous operation to complete (or timeout)
        /// </summary>
        public void Wait(Task task)
        {
            this._connectionMultiplexer.Wait(task);
        }

        /// <summary>
        ///     Wait for a given asynchronous operation to complete (or timeout)
        /// </summary>
        public T Wait<T>(Task<T> task)
        {
            return this._connectionMultiplexer.Wait(task);
        }

        /// <summary>
        ///     Wait for the given asynchronous operations to complete (or timeout)
        /// </summary>
        public void WaitAll(params Task[] tasks)
        {
            this._connectionMultiplexer.WaitAll(tasks);
        }

        /// <summary>Compute the hash-slot of a specified key</summary>
        public int HashSlot(RedisKey key)
        {
            return this._connectionMultiplexer.HashSlot(key);
        }

        /// <summary>
        ///     Obtain a pub/sub subscriber connection to the specified server
        /// </summary>
        public ISubscriber GetSubscriber(object asyncState = null)
        {
            return this._connectionMultiplexer.GetSubscriber(asyncState);
        }

        /// <summary>
        ///     Obtain an interactive connection to a database inside redis
        /// </summary>
        public IDatabase GetDatabase(int db = -1, object asyncState = null)
        {
            return this._connectionMultiplexer.GetDatabase(db, asyncState);
        }

        /// <summary>Obtain a configuration API for an individual server</summary>
        public IServer GetServer(string host, int port, object asyncState = null)
        {
            return this._connectionMultiplexer.GetServer(host, port, asyncState);
        }

        /// <summary>Obtain a configuration API for an individual server</summary>
        public IServer GetServer(string hostAndPort, object asyncState = null)
        {
            return this._connectionMultiplexer.GetServer(hostAndPort, asyncState);
        }

        /// <summary>Obtain a configuration API for an individual server</summary>
        public IServer GetServer(IPAddress host, int port)
        {
            return this._connectionMultiplexer.GetServer(host, port);
        }

        /// <summary>Obtain a configuration API for an individual server</summary>
        public IServer GetServer(EndPoint endpoint, object asyncState = null)
        {
            return this._connectionMultiplexer.GetServer(endpoint, asyncState);
        }

        /// <summary>
        ///     Reconfigure the current connections based on the existing configuration
        /// </summary>
        public Task<bool> ConfigureAsync(TextWriter log = null)
        {
            return this._connectionMultiplexer.ConfigureAsync(log);
        }

        /// <summary>
        ///     Reconfigure the current connections based on the existing configuration
        /// </summary>
        public bool Configure(TextWriter log = null)
        {
            return this._connectionMultiplexer.Configure(log);
        }

        /// <summary>
        ///     Provides a text overview of the status of all connections
        /// </summary>
        public string GetStatus()
        {
            return this._connectionMultiplexer.GetStatus();
        }

        /// <summary>
        ///     Provides a text overview of the status of all connections
        /// </summary>
        public void GetStatus(TextWriter log)
        {
            this._connectionMultiplexer.GetStatus(log);
        }

        /// <summary>
        ///     Close all connections and release all resources associated with this object
        /// </summary>
        public void Close(bool allowCommandsToComplete = true)
        {
            this._connectionMultiplexer.Close(allowCommandsToComplete);
        }

        /// <summary>
        ///     Close all connections and release all resources associated with this object
        /// </summary>
        public Task CloseAsync(bool allowCommandsToComplete = true)
        {
            return this._connectionMultiplexer.CloseAsync(allowCommandsToComplete);
        }

        /// <summary>Obtains the log of unusual busy patterns</summary>
        public string GetStormLog()
        {
            return this._connectionMultiplexer.GetStormLog();
        }

        /// <summary>Resets the log of unusual busy patterns</summary>
        public void ResetStormLog()
        {
            this._connectionMultiplexer.ResetStormLog();
        }

        /// <summary>
        ///     Request all compatible clients to reconfigure or reconnect
        /// </summary>
        /// <returns>
        ///     The number of instances known to have received the message (however, the actual number can be higher; returns
        ///     -1 if the operation is pending)
        /// </returns>
        public long PublishReconfigure(CommandFlags flags = CommandFlags.None)
        {
            return this._connectionMultiplexer.PublishReconfigure(flags);
        }

        /// <summary>
        ///     Request all compatible clients to reconfigure or reconnect
        /// </summary>
        /// <returns>The number of instances known to have received the message (however, the actual number can be higher)</returns>
        public Task<long> PublishReconfigureAsync(CommandFlags flags = CommandFlags.None)
        {
            return this._connectionMultiplexer.PublishReconfigureAsync(flags);
        }

        /// <summary>
        ///     Gets the client-name that will be used on all new connections
        /// </summary>
        public string ClientName => this._connectionMultiplexer.ClientName;

        /// <summary>Gets the configuration of the connection</summary>
        public string Configuration => this._connectionMultiplexer.Configuration;

        /// <summary>Gets the timeout associated with the connections</summary>
        public int TimeoutMilliseconds => this._connectionMultiplexer.TimeoutMilliseconds;

        /// <summary>
        ///     The number of operations that have been performed on all connections
        /// </summary>
        public long OperationCount => this._connectionMultiplexer.OperationCount;

        /// <summary>
        ///     Gets or sets whether asynchronous operations should be invoked in a way that guarantees their original delivery
        ///     order
        /// </summary>
        public bool PreserveAsyncOrder
        {
            get => this._connectionMultiplexer.PreserveAsyncOrder;
            set => this._connectionMultiplexer.PreserveAsyncOrder = value;
        }

        /// <summary>Indicates whether any servers are connected</summary>
        public bool IsConnected => this._connectionMultiplexer.IsConnected;

        /// <summary>
        ///     Should exceptions include identifiable details? (key names, additional .Data annotations)
        /// </summary>
        public bool IncludeDetailInExceptions
        {
            get => this._connectionMultiplexer.IncludeDetailInExceptions;
            set => this._connectionMultiplexer.IncludeDetailInExceptions = value;
        }

        /// <summary>
        ///     Limit at which to start recording unusual busy patterns (only one log will be retained at a time;
        ///     set to a negative value to disable this feature)
        /// </summary>
        public int StormLogThreshold
        {
            get => this._connectionMultiplexer.StormLogThreshold;
            set => this._connectionMultiplexer.StormLogThreshold = value;
        }

        event EventHandler<RedisErrorEventArgs> IConnectionMultiplexer.ErrorMessage
        {
            add => this._connectionMultiplexer.ErrorMessage += value;
            remove => this._connectionMultiplexer.ErrorMessage -= value;
        }

        event EventHandler<ConnectionFailedEventArgs> IConnectionMultiplexer.ConnectionFailed
        {
            add { this._connectionMultiplexer.ConnectionFailed += value; }
            remove { this._connectionMultiplexer.ConnectionFailed -= value; }
        }

        event EventHandler<InternalErrorEventArgs> IConnectionMultiplexer.InternalError
        {
            add { this._connectionMultiplexer.InternalError += value; }
            remove { this._connectionMultiplexer.InternalError -= value; }
        }

        event EventHandler<ConnectionFailedEventArgs> IConnectionMultiplexer.ConnectionRestored
        {
            add { this._connectionMultiplexer.ConnectionRestored += value; }
            remove { this._connectionMultiplexer.ConnectionRestored -= value; }
        }

        event EventHandler<EndPointEventArgs> IConnectionMultiplexer.ConfigurationChanged
        {
            add { this._connectionMultiplexer.ConfigurationChanged += value; }
            remove { this._connectionMultiplexer.ConfigurationChanged -= value; }
        }

        event EventHandler<EndPointEventArgs> IConnectionMultiplexer.ConfigurationChangedBroadcast
        {
            add { this._connectionMultiplexer.ConfigurationChangedBroadcast += value; }
            remove { this._connectionMultiplexer.ConfigurationChangedBroadcast -= value; }
        }

        event EventHandler<HashSlotMovedEventArgs> IConnectionMultiplexer.HashSlotMoved
        {
            add { this._connectionMultiplexer.HashSlotMoved += value; }
            remove { this._connectionMultiplexer.HashSlotMoved -= value; }
        }
    }
}