using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using CachingFramework.Core.Interface;

namespace CachingFramework.Core.Interceptions
{
    public class MethodResultCache
    {
        private readonly string _methodName;
        private ICache _cache;
        private readonly TimeSpan _expirationPeriod;
        private static readonly Dictionary<string, MethodResultCache> MethodCaches = new Dictionary<string, MethodResultCache>();
        private CacheType _cacheType;

        public MethodResultCache(string methodName, int expirationPeriod = 30, CacheType cacheType = CacheType.Memory)
        {
            _methodName = methodName;
            _expirationPeriod = new TimeSpan(0, 0, expirationPeriod, 0);
            _cache = Cache.Get(cacheType);
        }

        private string GetCacheKey(IEnumerable<object> arguments)
        {
            var key = string.Format(
              "{0}({1})",
              _methodName,
              string.Join(", ", arguments.Select(x => x != null ? x.ToString() : "<Null>")));
            return key;
        }

        public void CacheCallResult(object result, IEnumerable<object> arguments)
        {
            _cache.Set(GetCacheKey(arguments), result, _expirationPeriod);
        }

        public object GetCachedResult(IEnumerable<object> arguments)
        {
            return _cache.Get(GetCacheKey(arguments));
        }

        public void ClearCachedResults(CacheType cacheType)
        {
            _cache = null;
            _cache = Cache.Get(cacheType);
        }

        public static MethodResultCache GetCache(string methodName, CacheType cacheType)
        {
            if (MethodCaches.ContainsKey(methodName))
                return MethodCaches[methodName];
            var cache = new MethodResultCache(methodName, 30, cacheType);
            MethodCaches.Add(methodName, cache);
            return cache;
        }

        public static MethodResultCache GetCache(MemberInfo methodInfo, CacheType cacheType)
        {
            var methodName = string.Format("{0}.{1}.{2}",
                                           methodInfo.ReflectedType.Namespace,
                                           methodInfo.ReflectedType.Name,
                                           methodInfo.Name);
            return GetCache(methodName, cacheType);
        }
    }
}
