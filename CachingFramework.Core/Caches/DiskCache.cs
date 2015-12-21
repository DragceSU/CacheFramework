// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiskCache.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

 //using Sixeyed.Core.Configuration;
//using Sixeyed.Core.Cryptography;
//using Sixeyed.Core.Logging;
namespace CachingFramework.Core.Caches
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// </summary>
    public class DiskCache : CacheBase
    {
        /// <summary>
        /// </summary>
        private string _directory;

        /// <summary>
        /// </summary>
        private bool _directoryValid = true;

        /// <summary>
        /// </summary>
        public override CacheType CacheType
        {
            get
            {
                return CacheType.Disk;
            }
        }

        /// <summary>
        /// </summary>
        public override void Initialise()
        {
            try
            {
                // check directory exists
                this._directory = @"E:\GitHub\DickCache";
                if (!Directory.Exists(this._directory))
                {
                    this._directoryValid = false;

                    // Log.Error("DiskCache - directory specified in diskCache.path config: {0} does not exist. Not caching.", _directory);
                }
                else if (this.HasExceededQuota())
                {
                    // Log.Warn("DiskCache - exceeded quote for directory specified in diskCache.path config: {0}. Not caching.", _directory);
                }
            }
            catch (Exception ex)
            {
                this._directoryValid = false;

                // Log.Error("DiskCache - error checking diskCache.path config: {0}, message: {1}. Not caching.", _directory, ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <param name="value">
        /// </param>
        protected override void SetInternal(string key, object value)
        {
            this.SetInternal(key, value, null);
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
            try
            {
                this.SetInternal(key, value, DateTime.UtcNow.Add(validFor));
            }
            catch (Exception ex)
            {
                // do nothing
            }
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
            this.SetInternal(key, value, expiresAt);
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <param name="value">
        /// </param>
        /// <param name="expiresAt">
        /// </param>
        private void SetInternal(string key, object value, DateTime? expiresAt)
        {
            try
            {
                if (this._directoryValid && !this.HasExceededQuota())
                {
                    // check for a non-expiring cache:
                    var cachePath = this.GetFilePath(key);
                    if (File.Exists(cachePath))
                    {
                        File.Delete(cachePath);
                    }

                    // check for other caches:
                    var fileName = this.GetFileNameSearchPattern(key);
                    var existingCaches = Directory.EnumerateFiles(this._directory, fileName);
                    foreach (var existingCache in existingCaches)
                    {
                        File.Delete(Path.Combine(this._directory, existingCache));
                    }

                    var newCachePath = this.GetFilePath(key, expiresAt);
                    var item = ObjectToByteArray(value);
                    if (item != null)
                    {
                        File.WriteAllBytes(newCachePath, item);
                    }
                }
            }
            catch (Exception ex)
            {
                // do nothing
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="obj">
        /// </param>
        /// <returns>
        /// </returns>
        public static byte[] ObjectToByteArray(object obj)
        {
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="arrBytes">
        /// </param>
        /// <returns>
        /// </returns>
        public static object ByteArrayToObject(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <returns>
        /// </returns>
        protected override object GetInternal(string key)
        {
            object value = null;
            try
            {
                if (this._directoryValid)
                {
                    // check for a non-expiring cache:
                    var cachePath = this.GetFilePath(key);
                    if (!File.Exists(cachePath))
                    {
                        cachePath = null;

                        // check for expired caches:
                        var fileName = this.GetFileNameSearchPattern(key);
                        var existingCaches =
                            Directory.EnumerateFiles(this._directory, fileName).OrderByDescending(x => x);
                        if (existingCaches.Count() > 0)
                        {
                            var mostRecentCache = existingCaches.ElementAt(0);

                            // if the most recent cache is live, return it -
                            // format is {guid}.cache.{expiresAt}.expiry
                            if (mostRecentCache.EndsWith(".expiry"))
                            {
                                var expiresAt = mostRecentCache.Substring(mostRecentCache.IndexOf(".expiry") - 19, 19);
                                var expiresAtDate = expiresAt.Replace('-', '/').Replace('_', ':');
                                var expiryDate = DateTime.Parse(expiresAtDate);
                                if (expiryDate > DateTime.UtcNow)
                                {
                                    cachePath = Path.Combine(this._directory, mostRecentCache);
                                }
                            }
                        }
                    }

                    if (cachePath != null)
                    {
                        value = ByteArrayToObject(File.ReadAllBytes(cachePath));
                    }
                }
            }
            catch (Exception ex)
            {
                // do nothing
            }

            return value;
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        protected override void RemoveInternal(string key)
        {
            try
            {
                if (this._directoryValid)
                {
                    var path = this.GetFilePath(key);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
            }
            catch (Exception ex)
            {
                // do nothing
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <returns>
        /// </returns>
        protected override bool ExistsInternal(string key)
        {
            var exists = false;
            try
            {
                if (this._directoryValid)
                {
                    var path = this.GetFilePath(key);
                    exists = File.Exists(path);
                }
            }
            catch (Exception ex)
            {
                // do nothing
            }

            return exists;
        }

        /// <summary>
        /// </summary>
        /// <param name="cacheKey">
        /// </param>
        /// <returns>
        /// </returns>
        private static string GetFileName(string cacheKey)
        {
            return cacheKey + ".cache";
        }

        /// <summary>
        /// </summary>
        /// <param name="cacheKey">
        /// </param>
        /// <returns>
        /// </returns>
        private string GetFileNameSearchPattern(string cacheKey)
        {
            return GetFileName(cacheKey) + ".*";
        }

        /// <summary>
        /// </summary>
        /// <param name="cacheKey">
        /// </param>
        /// <returns>
        /// </returns>
        private string GetFilePath(string cacheKey)
        {
            return Path.Combine(this._directory, GetFileName(cacheKey));
        }

        /// <summary>
        /// </summary>
        /// <param name="cacheKey">
        /// </param>
        /// <param name="expiresAt">
        /// </param>
        /// <returns>
        /// </returns>
        private string GetFilePath(string cacheKey, DateTime? expiresAt)
        {
            var path = this.GetFilePath(cacheKey);
            if (expiresAt.HasValue)
            {
                path = string.Format("{0}.{1}.expiry", path, expiresAt.Value.ToString("yyyy-MM-ddTHH_mm_ss"));
            }

            return path;
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        private bool HasExceededQuota()
        {
            var size = new DirectoryInfo(this._directory).GetFiles().Sum(x => x.Length);
            return (size / (1024 * 1024)) > 20;
        }
    }
}