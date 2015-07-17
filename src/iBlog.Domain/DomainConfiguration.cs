// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DomainConfiguration.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The domain configuration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain
{
    using System.Configuration;

    using iBlog.Config;

    /// <summary>
    /// The domain configuration.
    /// </summary>
    public static class DomainConfiguration
    {
        #region Static Fields

        /// <summary>
        /// The blog settings.
        /// </summary>
        private static readonly SettingConfigSection BlogSettings =
            ConfigurationManager.GetSection("iBlogSettings") as SettingConfigSection;

        #endregion

        #region Public Properties

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