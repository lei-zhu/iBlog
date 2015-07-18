// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingConfigSection.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The setting config section.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Configuration
{
    using System.Configuration;

    /// <summary>
    /// The setting config section.
    /// </summary>
    public class SettingConfigSection : ConfigurationSection
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the cache duration.
        /// </summary>
        [ConfigurationProperty("cacheDuration")]
        public string CacheDuration
        {
            get
            {
                return (string)this["cacheDuration"];
            }

            set
            {
                this["cacheDuration"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        [ConfigurationProperty("connectionString", DefaultValue = "")]
        public string ConnectionString
        {
            get
            {
                return (string)this["connectionString"];
            }

            set
            {
                this["connectionString"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether enable mini profiler.
        /// </summary>
        [ConfigurationProperty("enableMiniProfiler", DefaultValue = false)]
        public bool EnableMiniProfiler
        {
            get
            {
                return (bool)this["enableMiniProfiler"];
            }

            set
            {
                this["enableMiniProfiler"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the theme.
        /// </summary>
        [ConfigurationProperty("theme")]
        public ThemeElement Theme
        {
            get
            {
                return (ThemeElement)this["theme"];
            }

            set
            {
                this["theme"] = value;
            }
        }

        #endregion
    }
}