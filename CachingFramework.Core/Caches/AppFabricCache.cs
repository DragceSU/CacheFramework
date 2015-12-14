﻿using System;
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
                servers[0] = new DataCacheServerEndpoint("ROCJ_EDMS_DEV01", 22233);
                // Setup datacache factory
                _factoryConfig = new DataCacheFactoryConfiguration();
                _factoryConfig.Servers = servers;
                // Create a configured DataCacheFactory object.
                _factory = new DataCacheFactory(_factoryConfig);

                // Get a cache client for the cache "NamedCache1".
                //DataCache myDefaultCache = mycacheFactory.GetCache("NamedCache1");
                _cache = _factory.GetDefaultCache();
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