// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISettingService.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The SettingService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Interfaces
{
    /// <summary>
    /// The SettingService interface.
    /// </summary>
    public interface ISettingService
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the blog admin email address.
        /// </summary>
        string BlogAdminEmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the blog caption.
        /// </summary>
        string BlogCaption { get; set; }

        /// <summary>
        /// Gets or sets the blog name.
        /// </summary>
        string BlogName { get; set; }

        /// <summary>
        /// Gets or sets the blog posts per page.
        /// </summary>
        int BlogPostsPerPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether blog site error email action.
        /// </summary>
        bool BlogSiteErrorEmailAction { get; set; }

        /// <summary>
        /// Gets or sets the blog smtp address.
        /// </summary>
        string BlogSmtpAddress { get; set; }

        /// <summary>
        /// Gets or sets the blog smtp password.
        /// </summary>
        string BlogSmtpPassword { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether blog social sharing.
        /// </summary>
        bool BlogSocialSharing { get; set; }

        /// <summary>
        /// Gets or sets the blog social sharing choice.
        /// </summary>
        int BlogSocialSharingChoice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether blog syntax highlighting.
        /// </summary>
        bool BlogSyntaxHighlighting { get; set; }

        /// <summary>
        /// Gets or sets the blog syntax scripts.
        /// </summary>
        string BlogSyntaxScripts { get; set; }

        /// <summary>
        /// Gets or sets the blog syntax theme.
        /// </summary>
        string BlogSyntaxTheme { get; set; }

        /// <summary>
        /// Gets or sets the blog theme.
        /// </summary>
        string BlogTheme { get; set; }

        /// <summary>
        /// Gets or sets the editor type.
        /// </summary>
        string EditorType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether installation complete.
        /// </summary>
        bool InstallationComplete { get; set; }

        /// <summary>
        /// Gets or sets the manage items per page.
        /// </summary>
        int ManageItemsPerPage { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get value.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetValue(string key);

        /// <summary>
        /// The update setting.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool UpdateSetting(string key, string value);

        #endregion
    }
}