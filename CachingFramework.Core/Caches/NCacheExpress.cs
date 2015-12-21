// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NCacheExpress.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

 //using Sixeyed.Core.Configuration;
//using Sixeyed.Core.Logging;
namespace CachingFramework.Core.Caches
{
    using System;

    using Alachisoft.NCache.Runtime;
    using Alachisoft.NCache.Web.Caching;

    /// <summary>
    ///     <see cref="ICache" /> implementation using NCacheExpress as the backing cache
    /// </summary>
    /// <remarks>
    ///     Uses CacheConfiguration setting "defaultCacheName" to determine the cache name.
    ///     Defaults to "Sixeyed.Core.Cache" if not set
    /// </remarks>
    public class NCacheExpress : CacheBase
    {
        /// <summary>
        /// </summary>
        private string _cacheName;

        /// <summary>
        /// </summary>
        public DateTime NoAbsoluteExpiry { get; set; }

        /// <summary>
        /// </summary>
        public TimeSpan NoSlidingExpiry { get; set; }

        /// <summary>
        ///     Returns the cache type
        /// </summary>
        public override CacheType CacheType
        {
            get
            {
                return CacheType.NCacheExpress;
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
            NCache.Caches[this._cacheName].Insert(
                key, 
                value, 
                this.NoAbsoluteExpiry, 
                this.NoSlidingExpiry, 
                CacheItemPriority.Default);
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
            NCache.Caches[this._cacheName].Insert(
                key, 
                value, 
                expiresAt, 
                this.NoSlidingExpiry, 
                CacheItemPriority.Default);
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
            NCache.Caches[this._cacheName].Insert(
                key, 
                value, 
                this.NoAbsoluteExpiry, 
                validFor, 
                CacheItemPriority.Default);
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <returns>
        /// </returns>
        protected override object GetInternal(string key)
        {
            return NCache.Caches[this._cacheName].Get(key);
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        protected override void RemoveInternal(string key)
        {
            NCache.Caches[this._cacheName].Remove(key);
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <returns>
        /// </returns>
        protected override bool ExistsInternal(string key)
        {
            return NCache.Caches[this._cacheName].Get(key) != null;
        }

        /// <summary>
        /// </summary>
        public override void Initialise()
        {
            if (this._cacheName == null)
            {
                this._cacheName = "EN310031.endava.net";

                // Log.Debug("NCacheExpress.Initialise - initialising with cacheName: {0}", _cacheName);
                this.NoAbsoluteExpiry = DateTime.MaxValue;
                this.NoSlidingExpiry = new TimeSpan(0);
                var initParams = new CacheInitParams();
                var cacheServerInfos = new CacheServerInfo[1];
                cacheServerInfos[0] = new CacheServerInfo("localhost", 8250);
                initParams.ServerList = cacheServerInfos;
                NCache.InitializeCache(this._cacheName);

                // NCache.InitializeCache(_cacheName, initParams);
            }
        }
    }
}