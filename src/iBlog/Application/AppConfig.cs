// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppConfig.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The app config.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Application
{
    using System.Configuration;

    using iBlog.Config;

    /// <summary>
    /// The app config.
    /// </summary>
    public static class AppConfig
    {
        #region Constants

        /// <summary>
        /// The default cache duration.
        /// </summary>
        private const int DefaultCacheDuration = 5;

        #endregion

        #region Static Fields

        /// <summary>
        /// The blog settings.
        /// </summary>
        private static readonly SettingConfigSection BlogSettings =
            ConfigurationManager.GetSection("iBlogSettings") as SettingConfigSection;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the cache duration.
        /// </summary>
        public static int CacheDuration
        {
            get
            {
                string cacheDuration = BlogSettings.CacheDuration;
                int parsedDuration;
                return int.TryParse(cacheDuration, out parsedDuration) ? parsedDuration : DefaultCacheDuration;
            }
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                string connectionString = BlogSettings.ConnectionString;
                return connectionString;
            }
        }

        #endregion
    }
}