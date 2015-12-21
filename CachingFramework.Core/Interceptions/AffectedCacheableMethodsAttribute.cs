// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AffectedCacheableMethodsAttribute.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace CachingFramework.Core.Interceptions
{
    using System;

    using PostSharp.Aspects;

    /// <summary>
    /// </summary>
    [Serializable]
    public class AffectedCacheableMethodsAttribute : MethodInterceptionAspect
    {
        /// <summary>
        /// </summary>
        private readonly string[] _affectedMethods;

        /// <summary>
        /// </summary>
        /// <param name="methods">
        /// </param>
        public AffectedCacheableMethodsAttribute(params string[] methods)
        {
            this._affectedMethods = methods;
        }

        /// <summary>
        /// </summary>
        public CacheType cacheType { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="args">
        /// </param>
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            base.OnInvoke(args);

            foreach (var mi in this._affectedMethods)
            {
                MethodResultCache.GetCache(mi, this.cacheType).ClearCachedResults(this.cacheType);
            }
        }
    }
}