﻿using System;
using System.Linq;
using System.Security.Principal;
using PostSharp.Aspects;

namespace CachingFramework.Core.Interceptions
{
    [Serializable]
    public class CacheableResultAttribute : MethodInterceptionAspect
    {
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var cache = MethodResultCache.GetCache(args.Method, cacheType);
            var arguments = args.Arguments.ToList();
            var result = cache.GetCachedResult(args.Arguments.ToList());
            if (result != null)
            {
                args.ReturnValue = result;
                return;
            }

            base.OnInvoke(args);

            cache.CacheCallResult(args.ReturnValue, arguments);
        }

        public CacheType cacheType { get; set; }
    }
}
