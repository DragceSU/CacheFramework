// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullCache.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CachingFramework.Core.Caches
{
    using System;

    /// <summary>
    ///     <see cref="ICache" /> implementation which does nothing
    /// </summary>
    /// <remarks>
    ///     Used when real caches are unavailable or disabled
    /// </remarks>
    public class NullCache : CacheBase
    {
        /// <summary>
        /// </summary>
        public override CacheType CacheType
        {
            get
            {
                return CacheType.Null;
            }
        }

        /// <summary>
        /// </summary>
        public override void Initialise()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <param name="value">
        /// </param>
        protected override void SetInternal(string key, object value)
        {
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
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <returns>
        /// </returns>
        protected override object GetInternal(string key)
        {
            return null;
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        protected override void RemoveInternal(string key)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <returns>
        /// </returns>
        protected override bool ExistsInternal(string key)
        {
            return false;
        }
    }
}