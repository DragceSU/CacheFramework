// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICache.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CachingFramework.Core.Interface
{
    using System;

    /// <summary>
    /// </summary>
    public interface ICache
    {
        /// <summary>
        ///     Return Cache Type
        /// </summary>
        CacheType CacheType { get; }

        /// <summary>
        ///     Initialization
        /// </summary>
        void Initialise();

        /// <summary>
        /// Insert/update a cache value
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <param name="value">
        /// </param>
        void Set(string key, object value);

        /// <summary>
        /// Insert/update a cache value with an expiry date
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <param name="value">
        /// </param>
        /// <param name="expiresAt">
        /// </param>
        void Set(string key, object value, DateTime expiresAt);

        /// <summary>
        /// Insert/update a cache value with a timespan
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <param name="value">
        /// </param>
        /// <param name="validFor">
        /// </param>
        void Set(string key, object value, TimeSpan validFor);

        /// <summary>
        /// Return value from cache
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <returns>
        /// Cached value or null
        /// </returns>
        object Get(string key);

        /// <summary>
        /// Removes the value for the given key from the cache
        /// </summary>
        /// <param name="key">
        /// </param>
        void Remove(string key);

        /// <summary>
        /// Check if value exists in cache
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <returns>
        /// </returns>
        bool Exists(string key);
    }
}