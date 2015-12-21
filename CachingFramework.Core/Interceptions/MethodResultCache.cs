// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MethodResultCache.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CachingFramework.Core.Interceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using CachingFramework.Core.Interface;

    /// <summary>
    /// </summary>
    public class MethodResultCache
    {
        /// <summary>
        /// </summary>
        private static readonly Dictionary<string, MethodResultCache> MethodCaches =
            new Dictionary<string, MethodResultCache>();

        /// <summary>
        /// </summary>
        private readonly TimeSpan _expirationPeriod;

        /// <summary>
        /// </summary>
        private readonly string _methodName;

        /// <summary>
        /// </summary>
        private ICache _cache;

        /// <summary>
        /// </summary>
        private CacheType _cacheType;

        /// <summary>
        /// </summary>
        /// <param name="methodName">
        /// </param>
        /// <param name="expirationPeriod">
        /// </param>
        /// <param name="cacheType">
        /// </param>
        public MethodResultCache(string methodName, int expirationPeriod = 30, CacheType cacheType = CacheType.Memory)
        {
            this._methodName = methodName;
            this._expirationPeriod = new TimeSpan(0, 0, expirationPeriod, 0);
            this._cache = Cache.Get(cacheType);
        }

        /// <summary>
        /// </summary>
        /// <param name="arguments">
        /// </param>
        /// <returns>
        /// </returns>
        private string GetCacheKey(IEnumerable<object> arguments)
        {
            var key = string.Format(
                "{0}({1})", 
                this._methodName, 
                string.Join(", ", arguments.Select(x => x != null ? x.ToString() : "<Null>")));
            return key;
        }

        /// <summary>
        /// </summary>
        /// <param name="result">
        /// </param>
        /// <param name="arguments">
        /// </param>
        public void CacheCallResult(object result, IEnumerable<object> arguments)
        {
            this._cache.Set(this.GetCacheKey(arguments), result, this._expirationPeriod);
        }

        /// <summary>
        /// </summary>
        /// <param name="arguments">
        /// </param>
        /// <returns>
        /// </returns>
        public object GetCachedResult(IEnumerable<object> arguments)
        {
            return this._cache.Get(this.GetCacheKey(arguments));
        }

        /// <summary>
        /// </summary>
        /// <param name="cacheType">
        /// </param>
        public void ClearCachedResults(CacheType cacheType)
        {
            this._cache = null;
            this._cache = Cache.Get(cacheType);
        }

        /// <summary>
        /// </summary>
        /// <param name="methodName">
        /// </param>
        /// <param name="cacheType">
        /// </param>
        /// <returns>
        /// </returns>
        public static MethodResultCache GetCache(string methodName, CacheType cacheType)
        {
            if (MethodCaches.ContainsKey(methodName))
            {
                return MethodCaches[methodName];
            }

            var cache = new MethodResultCache(methodName, 30, cacheType);
            MethodCaches.Add(methodName, cache);
            return cache;
        }

        /// <summary>
        /// </summary>
        /// <param name="methodInfo">
        /// </param>
        /// <param name="cacheType">
        /// </param>
        /// <returns>
        /// </returns>
        public static MethodResultCache GetCache(MemberInfo methodInfo, CacheType cacheType)
        {
            var methodName = string.Format(
                "{0}.{1}.{2}", 
                methodInfo.ReflectedType.Namespace, 
                methodInfo.ReflectedType.Name, 
                methodInfo.Name);
            return GetCache(methodName, cacheType);
        }
    }
}