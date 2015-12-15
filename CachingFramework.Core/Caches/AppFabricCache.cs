using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ApplicationServer.Caching;

namespace CachingFramework.Core.Caches
{
    public class AppFabricCache : CacheBase
    {
        private DataCacheFactory _factory;
        private DataCacheFactoryConfiguration _factoryConfig;
        private DataCacheTransportProperties _factoryTransport;

        //private DataCache _factorySecurity;
        private DataCache _cache;

        public override CacheType CacheType
        {
            get { return CacheType.AppFabric; }
        }

        public override void Initialise()
        {
            if (_cache == null)
            {
                // Define the hosts
                DataCacheServerEndpoint[] servers = new DataCacheServerEndpoint[1];
                servers[0] = new DataCacheServerEndpoint("EN310031.endava.net", 22233);
                // Setup datacache factory
                _factoryConfig = new DataCacheFactoryConfiguration();
                _factoryConfig.Servers = servers;

                _factoryTransport = new DataCacheTransportProperties()
                                        {
                                            ConnectionBufferSize = 131072,
                                            MaxBufferPoolSize = 268435456,
                                            MaxBufferSize = 8388608,
                                            MaxOutputDelay = new TimeSpan(0, 0, 2),
                                            ChannelInitializationTimeout = new TimeSpan(0, 0, 60),
                                            ReceiveTimeout = new TimeSpan(0, 0, 600)
                                        };
                _factoryConfig.TransportProperties = _factoryTransport;

                _factoryConfig.SecurityProperties = new DataCacheSecurity(DataCacheSecurityMode.None, DataCacheProtectionLevel.None);

                // Create a configured DataCacheFactory object.
                _factory = new DataCacheFactory(_factoryConfig);

                // Get a cache client for the cache "NamedCache1".
                //DataCache myDefaultCache = mycacheFactory.GetCache("NamedCache1");
                _cache = _factory.GetCache("default");
            }
        }

        protected override void SetInternal(string key, object value)
        {
            _cache.Put(key, value);
        }

        protected override void SetInternal(string key, object value, DateTime expiresAt)
        {
            SetInternal(key, value, new TimeSpan(expiresAt.Subtract(DateTime.Now).Ticks));
        }

        protected override void SetInternal(string key, object value, TimeSpan validFor)
        {
            _cache.Put(key, value, validFor);
        }

        protected override object GetInternal(string key)
        {
            return _cache.Get(key);
        }

        protected override void RemoveInternal(string key)
        {
            _cache.Remove(key);
        }

        protected override bool ExistsInternal(string key)
        {
            return Get(key) != null;
        }
    }
}
