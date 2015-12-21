// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CacheBase.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CachingFramework.Core
{
    using System;

    using CachingFramework.Core.Caches;
    using CachingFramework.Core.Interface;

    /// <summary>
    /// </summary>
    public abstract class CacheBase : ICache
    {
        /// <summary>
        /// </summary>
        private static NullCache _nullCache = new NullCache();

        /// <summary>
        /// </summary>
        private CacheBase Current
        {
            get
            {
                return this;
            }
        }

        /// <summary>
        /// </summary>
        public abstract CacheType CacheType { get; }

        /// <summary>
        /// </summary>
        public abstract void Initialise();

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <param name="value">
        /// </param>
        public void Set(string key, object value)
        {
            this.Current.SetInternal(key, value);
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <param name="value">
        /// </param>
        /// <param name="expiresAt">
        /// </param>
        public void Set(string key, object value, DateTime expiresAt)
        {
            this.Current.SetInternal(key, value, expiresAt);
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <param name="value">
        /// </param>
        /// <param name="validFor">
        /// </param>
        public void Set(string key, object value, TimeSpan validFor)
        {
            this.Current.SetInternal(key, value, validFor);
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <returns>
        /// </returns>
        public object Get(string key)
        {
            return this.Current.GetInternal(key);
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        public void Remove(string key)
        {
            this.Current.RemoveInternal(key);
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <returns>
        /// </returns>
        public bool Exists(string key)
        {
            return this.Current.ExistsInternal(key);
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <param name="value">
        /// </param>
        protected abstract void SetInternal(string key, object value);

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <param name="value">
        /// </param>
        /// <param name="expiresAt">
        /// </param>
        protected abstract void SetInternal(string key, object value, DateTime expiresAt);

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <param name="value">
        /// </param>
        /// <param name="validFor">
        /// </param>
        protected abstract void SetInternal(string key, object value, TimeSpan validFor);

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <returns>
        /// </returns>
        protected abstract object GetInternal(string key);

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        protected abstract void RemoveInternal(string key);

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <returns>
        /// </returns>
        protected abstract bool ExistsInternal(string key);
    }
}