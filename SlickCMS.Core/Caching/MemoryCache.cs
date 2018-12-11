using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.Caching;

namespace SlickCMS.Core.Caching
{
    internal class MemoryCache
    {
        internal static void Add(string cacheKey, object data)
        {
            // get the default memory cache
            var cache = System.Runtime.Caching.MemoryCache.Default;

            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now.AddMinutes(60);

            // add the item
            cache.Add(cacheKey, data, policy);
        }

        /// <summary>
        /// Returns the Cached object as the corresponding class
        /// Usage:
        ///     var cachedObject = Cortana.Core.Caching.MemoryCache.Get<List<string>>(cacheKey);
        /// </summary>
        internal static T Get<T>(string cacheKey) where T : class
        {
            var cache = System.Runtime.Caching.MemoryCache.Default;

            if (cache.Contains(cacheKey))
                return (T)cache.Get(cacheKey);

            return null;
        }

        /// <summary>
        /// Returns the Cached object
        /// Usage:
        ///     var cachedObject = (List<string>)Cortana.Core.Caching.MemoryCache.Get(cacheKey);
        /// </summary>
        internal static object Get(string cacheKey)
        {
            var cache = System.Runtime.Caching.MemoryCache.Default;

            if (cache.Contains(cacheKey))
                return cache.Get(cacheKey);

            return null;
        }

        internal static void Remove(string cacheKey)
        {
            var cache = System.Runtime.Caching.MemoryCache.Default;
            cache.Remove(cacheKey);
        }
    }
}
