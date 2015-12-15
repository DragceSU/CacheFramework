using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CachingFramework.Core.Interface;

namespace CachingFramework.Core
{
    using CachingFramework.Core.Caches;

    public static class Cache
    {
        public static ICache Get(CacheType cacheType)
        {
            ICache cache = new NullCache();
            try
            {
                //var caches = Container.GetAll<ICache>();
                //cache = (from c in caches
                //         where c.CacheType == cacheType
                //         select c).Single();
                switch (cacheType)
                {
                    case CacheType.Memory:
                        cache = new MemoryCache();
                        break;
                    case CacheType.AppFabric:
                        cache = new AppFabricCache();
                        break;
                    case CacheType.Disk:
                        cache = new DiskCache();
                        break;
                    case CacheType.NCacheExpress:
                        cache = new NCacheExpress();
                        break;
                    case CacheType.Null:
                        cache = new NullCache();
                        break;
                }
                cache.Initialise();
            }
            catch (Exception ex)
            {
                //Log.Warn("Failed to instantiate cache of type: {0}, using null cache. Exception: {1}", cacheType, ex);
                cache = new NullCache();
            }
            return cache;
        }

        public static ICache Default
        {
            get
            {
                return Get(CacheType.Memory);
            }
        }

        public static ICache Memory
        {
            get
            {
                return Get(CacheType.Memory);
            }
        }

        public static ICache NCacheExpress
        {
            get
            {
                return Get(CacheType.NCacheExpress);
            }
        }

        public static ICache AppFabric
        {
            get
            {
                return Get(CacheType.AppFabric);
            }
        }

        public static ICache Disk
        {
            get
            {
                return Get(CacheType.Disk);
            }
        }
    }
}
