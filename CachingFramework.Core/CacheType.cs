using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachingFramework.Core
{
    public enum CacheType
    {
        /// <summary>
        /// No cache type set
        /// </summary>
        Null = 0,

        Memory,

        NCacheExpress,

        AppFabric,

        Disk
    }
}
