// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MemoryCache.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using sys = System.Runtime.Caching;

// using Sixeyed.Core.Configuration;

// using Sixeyed.Core.Logging;
namespace CachingFramework.Core.Caches
{
    using System;

    using CachingFramework.Core.Interface;

    /// <summary>
    ///     <see cref="ICache" /> implementation using .NET MemoryCache as the backing cache
    /// </summary>
    /// <remarks>
    ///     Uses CacheConfiguration setting "defaultCacheName" to determine the cache name.
    ///     Defaults to "Sixeyed.Core.Cache" if not set
    /// </remarks>
    public class MemoryCache : CacheBase
    {
        /// <summary>
        /// </summary>
        private sys.MemoryCache _cache;

        /// <summary>
        /// </summary>
        public DateTime AbsoluteExpiry { get; set; }

        /// <summary>
        /// </summary>
        public TimeSpan SlidingExpiry { get; set; }

        /// <summary>
        ///     Returns the cache type
        /// </summary>
        public override CacheType CacheType
        {
            get
            {
                return CacheType.Memory;
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
            var policy = new sys.CacheItemPolicy();
            this.Set(key, value, policy);
        }

        /// <summary>
        /// Insert or update a cache value with a fixed lifetime
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <param name="value">
        /// </param>
        /// <param name="lifespan">
        /// </param>
        protected override void SetInternal(string key, object value, TimeSpan lifespan)
        {
            var policy = new sys.CacheItemPolicy();
            policy.SlidingExpiration = lifespan;
            this.Set(key, value, policy);
        }

        /// <summary>
        /// Insert or update a cache value with an expiry date
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <param name="value">
        /// </param>
        /// <param name="expiresAt">
        /// </param>
        protected override void SetInternal(string key, object value, DateTime expiresAt)
        {
            var policy = new sys.CacheItemPolicy();
            policy.AbsoluteExpiration = expiresAt;
            this.Set(key, value, policy);
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <param name="value">
        /// </param>
        /// <param name="policy">
        /// </param>
        private void Set(string key, object value, sys.CacheItemPolicy policy)
        {
            this._cache.Set(key, value, policy);
        }

        /// <summary>
        /// Retrieve a value from cache
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <returns>
        /// Cached value or null
        /// </returns>
        protected override object GetInternal(string key)
        {
            return this._cache[key];
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <returns>
        /// </returns>
        protected override bool ExistsInternal(string key)
        {
            return this._cache.Contains(key);
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        protected override void RemoveInternal(string key)
        {
            if (this.Exists(key))
            {
                this._cache.Remove(key);
            }
        }

        /// <summary>
        /// </summary>
        public override void Initialise()
        {
            if (this._cache == null)
            {
                // Log.Debug("MemoryCache.Initialise - initialising with cacheName: {0}", CacheConfiguration.Current.DefaultCacheName);
                this._cache = new sys.MemoryCache("defaultCacheName");
            }
        }
    }
}