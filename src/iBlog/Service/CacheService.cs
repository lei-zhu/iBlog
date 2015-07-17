// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CacheService.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The cache service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Service
{
    using System;
    using System.Web;
    using System.Web.Caching;

    using iBlog.Application;
    using iBlog.Domain.Interfaces;

    /// <summary>
    /// The cache service.
    /// </summary>
    public class CacheService : ICacheService
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="cacheID">
        /// The cache id.
        /// </param>
        /// <param name="getItemCallback">
        /// The get item callback.
        /// </param>
        /// <typeparam name="T">
        /// The generic type.
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T Get<T>(string cacheID, Func<T> getItemCallback) where T : class
        {
            var item = HttpRuntime.Cache.Get(cacheID) as T;
            if (item == null)
            {
                item = getItemCallback();
                HttpContext.Current.Cache.Insert(
                    cacheID, 
                    item, 
                    null, 
                    DateTime.Now.AddMinutes(AppConfig.CacheDuration), 
                    Cache.NoSlidingExpiration);
            }

            return item;
        }

        #endregion
    }
}