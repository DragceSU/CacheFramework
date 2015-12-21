// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CacheableResultAttribute.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace CachingFramework.Core.Interceptions
{
    using System;
    using System.Linq;

    using PostSharp.Aspects;

    /// <summary>
    /// </summary>
    [Serializable]
    public class CacheableResultAttribute : MethodInterceptionAspect
    {
        /// <summary>
        /// </summary>
        public CacheType cacheType { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="args">
        /// </param>
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var cache = MethodResultCache.GetCache(args.Method, this.cacheType);
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
    }
}