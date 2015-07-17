// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingService.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The setting service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Implements
{
    using System.Data.Linq;
    using System.Globalization;
    using System.Linq;

    using iBlog.Domain.Entities;
    using iBlog.Domain.Interfaces;

    /// <summary>
    /// The setting service.
    /// </summary>
    public class SettingService : ISettingService
    {
        #region Fields

        /// <summary>
        /// The context.
        /// </summary>
        private readonly DataContext context;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingService"/> class.
        /// </summary>
        public SettingService()
        {
            this.context = new DataContext(DomainConfiguration.ConnectionString);

            this.LoadSettings();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the blog admin email address.
        /// </summary>
        public string BlogAdminEmailAddress
        {
            get
            {
                string blogEmail = this.GetValue("BlogAdminEmailAddress");
                return blogEmail ?? string.Empty;
            }

            set
            {
                string blogEmail = GetValueInternal(value) ?? string.Empty;
                this.UpdateSetting("BlogAdminEmailAddress", blogEmail);
            }
        }

        /// <summary>
        /// Gets or sets the blog caption.
        /// </summary>
        public string BlogCaption
        {
            get
            {
                string blogCaption = this.GetValue("BlogCaption");
                return blogCaption ?? ".Net blog";
            }

            set
            {
                string blogCaption = GetValueInternal(value) ?? ".Net blog";
                this.UpdateSetting("BlogCaption", blogCaption);
            }
        }

        /// <summary>
        /// Gets or sets the blog name.
        /// </summary>
        public string BlogName
        {
            get
            {
                string blogName = this.GetValue("BlogName");
                return blogName ?? "iBlog";
            }

            set
            {
                string blogName = GetValueInternal(value) ?? "iBlog";
                this.UpdateSetting("BlogName", blogName);
            }
        }

        /// <summary>
        /// Gets or sets the blog posts per page.
        /// </summary>
        public int BlogPostsPerPage
        {
            get
            {
                int postsPerPage;
                if (!int.TryParse(this.GetValue("BlogPostsPerPage"), out postsPerPage))
                {
                    postsPerPage = 5;
                }

                return postsPerPage;
            }

            set
            {
                this.UpdateSetting("BlogPostsPerPage", value.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether blog site error email action.
        /// </summary>
        public bool BlogSiteErrorEmailAction
        {
            get
            {
                string blogSiteErrorAction = this.GetValue("BlogSiteErrorEmailAction");
                bool result;
                if (!bool.TryParse(blogSiteErrorAction, out result))
                {
                    result = false;
                }

                return result;
            }

            set
            {
                this.UpdateSetting("BlogSiteErrorEmailAction", value.ToString());
            }
        }

        /// <summary>
        /// Gets or sets the blog smtp address.
        /// </summary>
        public string BlogSmtpAddress
        {
            get
            {
                string smtpAddress = this.GetValue("BlogSmtpAddress");
                return smtpAddress ?? string.Empty;
            }

            set
            {
                string smtpAddress = GetValueInternal(value) ?? string.Empty;
                this.UpdateSetting("BlogSmtpAddress", smtpAddress);
            }
        }

        /// <summary>
        /// Gets or sets the blog smtp password.
        /// </summary>
        public string BlogSmtpPassword
        {
            get
            {
                string smtpAddress = this.GetValue("BlogSmtpPassword");
                return smtpAddress ?? string.Empty;
            }

            set
            {
                string smtpAddress = GetValueInternal(value) ?? string.Empty;
                this.UpdateSetting("BlogSmtpPassword", smtpAddress);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether blog social sharing.
        /// </summary>
        public bool BlogSocialSharing
        {
            get
            {
                string blogSharing = this.GetValue("BlogSocialSharing");
                bool result;
                if (!bool.TryParse(blogSharing, out result))
                {
                    result = false;
                }

                return result;
            }

            set
            {
                this.UpdateSetting("BlogSocialSharing", value.ToString());
            }
        }

        /// <summary>
        /// Gets or sets the blog social sharing choice.
        /// </summary>
        public int BlogSocialSharingChoice
        {
            get
            {
                int blogSocialSharingChoice;
                if (!int.TryParse(this.GetValue("BlogSocialSharingChoice"), out blogSocialSharingChoice))
                {
                    blogSocialSharingChoice = 2;
                }

                return blogSocialSharingChoice;
            }

            set
            {
                this.UpdateSetting("BlogSocialSharingChoice", value.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether blog syntax highlighting.
        /// </summary>
        public bool BlogSyntaxHighlighting
        {
            get
            {
                string blogSharing = this.GetValue("BlogSyntaxHighlighting");
                bool result;
                if (!bool.TryParse(blogSharing, out result))
                {
                    result = false;
                }

                return result;
            }

            set
            {
                this.UpdateSetting("BlogSyntaxHighlighting", value.ToString());
            }
        }

        /// <summary>
        /// Gets or sets the blog syntax scripts.
        /// </summary>
        public string BlogSyntaxScripts
        {
            get
            {
                string blogSyntaxScripts = this.GetValue("BlogSyntaxScripts");
                return blogSyntaxScripts ?? "CSharp";
            }

            set
            {
                string blogSyntaxScripts = GetValueInternal(value) ?? "CSharp";
                this.UpdateSetting("BlogSyntaxScripts", blogSyntaxScripts);
            }
        }

        /// <summary>
        /// Gets or sets the blog syntax theme.
        /// </summary>
        public string BlogSyntaxTheme
        {
            get
            {
                string blogTheme = this.GetValue("BlogSyntaxTheme");
                return blogTheme ?? "Default";
            }

            set
            {
                string blogTheme = GetValueInternal(value) ?? "Default";
                this.UpdateSetting("BlogSyntaxTheme", blogTheme);
            }
        }

        /// <summary>
        /// Gets or sets the blog theme.
        /// </summary>
        public string BlogTheme
        {
            get
            {
                string blogTheme = this.GetValue("BlogTheme");
                return blogTheme ?? "PerfectBlemish";
            }

            set
            {
                string blogTheme = GetValueInternal(value) ?? "PerfectBlemish";
                this.UpdateSetting("BlogTheme", blogTheme);
            }
        }

        /// <summary>
        /// Gets or sets the editor type.
        /// </summary>
        public string EditorType
        {
            get
            {
                string editorType = this.GetValue("EditorType");
                return editorType ?? string.Empty;
            }

            set
            {
                string editorType = GetValueInternal(value) ?? string.Empty;
                this.UpdateSetting("EditorType", editorType);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether installation complete.
        /// </summary>
        public bool InstallationComplete
        {
            get
            {
                string installationComplete = this.GetValue("InstallationComplete");
                bool result;
                if (!bool.TryParse(installationComplete, out result))
                {
                    result = false;
                }

                return result;
            }

            set
            {
                this.UpdateSetting("InstallationComplete", value.ToString());
            }
        }

        /// <summary>
        /// Gets or sets the manage items per page.
        /// </summary>
        public int ManageItemsPerPage
        {
            get
            {
                int manageItemsPerPage;
                if (!int.TryParse(this.GetValue("ManageItemsPerPage"), out manageItemsPerPage))
                {
                    manageItemsPerPage = 5;
                }

                return manageItemsPerPage;
            }

            set
            {
                this.UpdateSetting("ManageItemsPerPage", value.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Gets the setting table.
        /// </summary>
        public Table<SettingEntity> SettingTable { get; private set; }

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
        public string GetValue(string key)
        {
            string value = null;

            SettingEntity setting = this.SettingTable.SingleOrDefault(s => s.KeyName == key);
            if (setting != null)
            {
                value = setting.KeyValue;
            }

            return value;
        }

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
        public bool UpdateSetting(string key, string value)
        {
            SettingEntity setting = this.SettingTable.Single(s => s.KeyName == key);
            if (setting != null)
            {
                setting.KeyValue = value;
                this.context.SubmitChanges();

                return true;
            }

            return false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get value internal.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetValueInternal(string value)
        {
            string valueInternal = value;
            if (value == null || value.Trim() == string.Empty)
            {
                valueInternal = null;
            }

            return valueInternal;
        }

        /// <summary>
        /// The load settings.
        /// </summary>
        private void LoadSettings()
        {
            this.SettingTable = this.context.GetTable<SettingEntity>();
        }

        #endregion
    }
}