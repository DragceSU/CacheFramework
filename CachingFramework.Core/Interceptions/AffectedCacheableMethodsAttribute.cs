using System;
using System.Reflection;
using PostSharp.Aspects;

namespace CachingFramework.Core.Interceptions
{
    [Serializable]
    public class AffectedCacheableMethodsAttribute : MethodInterceptionAspect
    {
        private readonly string[] _affectedMethods;

        public AffectedCacheableMethodsAttribute(params string[] methods)
        {
            _affectedMethods = methods;
        }

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            base.OnInvoke(args);

            foreach (var mi in _affectedMethods)
                MethodResultCache.GetCache(mi, cacheType).ClearCachedResults(cacheType);
        }

        public CacheType cacheType { get; set; }
    }
}
