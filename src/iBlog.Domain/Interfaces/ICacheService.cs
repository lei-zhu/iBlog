// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICacheService.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The CacheService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Interfaces
{
    using System;

    /// <summary>
    /// The CacheService interface.
    /// </summary>
    public interface ICacheService
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
        T Get<T>(string cacheID, Func<T> getItemCallback) where T : class;

        #endregion
    }
}