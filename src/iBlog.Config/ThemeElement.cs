// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThemeElement.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The theme element.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Config
{
    using System.Configuration;

    /// <summary>
    /// The theme element.
    /// </summary>
    public class ThemeElement : ConfigurationElement
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the selected theme.
        /// </summary>
        [ConfigurationProperty("selectedTheme")]
        public string SelectedTheme
        {
            get
            {
                return (string)this["selectedTheme"];
            }

            set
            {
                this["selectedTheme"] = value;
            }
        }

        #endregion
    }
}