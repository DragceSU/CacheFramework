// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppFabricCache.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace CachingFramework.Core.Caches
{
    using System;

    using Microsoft.ApplicationServer.Caching;

    /// <summary>
    /// </summary>
    public class AppFabricCache : CacheBase
    {
        // private DataCache _factorySecurity;
        /// <summary>
        /// </summary>
        private DataCache _cache;

        /// <summary>
        /// </summary>
        private DataCacheFactory _factory;

        /// <summary>
        /// </summary>
        private DataCacheFactoryConfiguration _factoryConfig;

        /// <summary>
        /// </summary>
        private DataCacheTransportProperties _factoryTransport;

        /// <summary>
        /// </summary>
        public override CacheType CacheType
        {
            get
            {
                return CacheType.AppFabric;
            }
        }

        /// <summary>
        /// </summary>
        public override void Initialise()
        {
            if (this._cache == null)
            {
                // Define the hosts
                var servers = new DataCacheServerEndpoint[1];
                servers[0] = new DataCacheServerEndpoint("EN310031.endava.net", 22233);

                // Setup datacache factory
                this._factoryConfig = new DataCacheFactoryConfiguration();
                this._factoryConfig.Servers = servers;

                this._factoryTransport = new DataCacheTransportProperties
                                             {
                                                 ConnectionBufferSize = 13107252, 
                                                 MaxBufferPoolSize = 26843545652, 
                                                 MaxBufferSize = 838860852, 
                                                 MaxOutputDelay = new TimeSpan(0, 0, 2), 
                                                 ChannelInitializationTimeout =
                                                     new TimeSpan(0, 0, 60), 
                                                 ReceiveTimeout = new TimeSpan(0, 0, 600)
                                             };
                this._factoryConfig.TransportProperties = this._factoryTransport;

                this._factoryConfig.SecurityProperties = new DataCacheSecurity(
                    DataCacheSecurityMode.None, 
                    DataCacheProtectionLevel.None);

                this._factory = new DataCacheFactory(this._factoryConfig);
                this._cache = this._factory.GetCache("default");
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <param name="value">
        /// </param>
        protected override void SetInternal(string key, object value)
        {
            this._cache.Put(key, value);
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <param name="value">
        /// </param>
        /// <param name="expiresAt">
        /// </param>
        protected override void SetInternal(string key, object value, DateTime expiresAt)
        {
            this.SetInternal(key, value, new TimeSpan(expiresAt.Subtract(DateTime.Now).Ticks));
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <param name="value">
        /// </param>
        /// <param name="validFor">
        /// </param>
        protected override void SetInternal(string key, object value, TimeSpan validFor)
        {
            this._cache.Put(key, value, validFor);
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <returns>
        /// </returns>
        protected override object GetInternal(string key)
        {
            return this._cache.Get(key);
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        protected override void RemoveInternal(string key)
        {
            this._cache.Remove(key);
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <returns>
        /// </returns>
        protected override bool ExistsInternal(string key)
        {
            return this.Get(key) != null;
        }
    }
}