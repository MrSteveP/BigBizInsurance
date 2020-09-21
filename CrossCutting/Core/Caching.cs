using Microsoft.Extensions.Caching.Memory;
using System;
using System.Runtime.Caching;
using Z.EntityFramework.Plus;

namespace CrossCutting.Core
{
    /// <summary>
    /// Sample
    /// 	using (var context = new EntityContext())
    ///{
    ///	// The query is cached using default QueryCacheManager options
    ///	var task1 = context.Customers.Where(x => x.IsActive).FromCacheAsync();
    ///}
    /// </summary>
    public static class Caching //: ICachingService
    {


        /// <summary>
        ///     The cache policy.
        /// </summary>
        /// 
        //this.applicationSettingsService.CacheItemPolicyDurationInHours
        public static MemoryCacheEntryOptions CachePolicy =>
            new MemoryCacheEntryOptions
            {
                SlidingExpiration =
                        TimeSpan.FromHours(10)
            };

        /// <summary>
        ///     The reports cache policy.
        /// </summary>
        public static CacheItemPolicy ReportsCachePolicy => new CacheItemPolicy { SlidingExpiration = TimeSpan.FromHours(1) };

        /// <summary>
        /// The clear cache.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        public static void ClearCache(string key)
        {
            QueryCacheManager.ExpireTag(key);
        }

        /// <summary>
        ///     The keys.
        /// </summary>
        public class Keys
        {
            /// <summary>
            ///     The all cache keys.
            /// </summary>
            public const string AllCacheKeys = "AllCacheKeys";

            public const string SystemSettings = "SystemSettings";
            public const string AttachmentTypes = "AttachmentTypes";

        }
    }

}
