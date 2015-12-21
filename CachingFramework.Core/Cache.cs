// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Cache.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace CachingFramework.Core
{
    using System;
    using System.Linq;

    using CachingFramework.Core.Caches;
    using CachingFramework.Core.Interface;

    using Ninject;

    /// <summary>
    /// </summary>
    public static class Cache
    {
        /// <summary>
        /// </summary>
        private static IKernel _kernel;

        /// <summary>
        /// </summary>
        public static ICache Default
        {
            get
            {
                return Get(CacheType.Memory);
            }
        }

        /// <summary>
        /// </summary>
        public static ICache Memory
        {
            get
            {
                return Get(CacheType.Memory);
            }
        }

        /// <summary>
        /// </summary>
        public static ICache NCacheExpress
        {
            get
            {
                return Get(CacheType.NCacheExpress);
            }
        }

        /// <summary>
        /// </summary>
        public static ICache AppFabric
        {
            get
            {
                return Get(CacheType.AppFabric);
            }
        }

        /// <summary>
        /// </summary>
        public static ICache Disk
        {
            get
            {
                return Get(CacheType.Disk);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="cacheType">
        /// </param>
        /// <returns>
        /// </returns>
        public static ICache Get(CacheType cacheType)
        {
            ICache cache = new NullCache();
            try
            {
                var caches = Container.Container.GetAll<CacheBase>();
                cache = (from c in caches where c.CacheType == cacheType select c).Single();
                cache.Initialise();
            }
            catch (Exception ex)
            {
                cache = new NullCache();
            }

            return cache;
        }
    }
}