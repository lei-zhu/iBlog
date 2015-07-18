// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BlogController.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The blog controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Controllers
{
    using System;
    using System.Configuration;
    using System.Web.Mvc;

    using iBlog.Application;
    using iBlog.Configuration;
    using iBlog.Domain.Interfaces;
    using iBlog.Models;

    /// <summary>
    ///     The blog controller.
    /// </summary>
    public class BlogController : Controller
    {
        #region Fields

        /// <summary>
        /// The blog settings.
        /// </summary>
        private readonly SettingConfigSection blogSettings =
            ConfigurationManager.GetSection("iBlogSettings") as SettingConfigSection;

        /// <summary>
        /// The setting service.
        /// </summary>
        private readonly ISettingService settingService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogController"/> class.
        /// </summary>
        /// <param name="setting">
        /// The setting.
        /// </param>
        public BlogController(ISettingService setting)
        {
            this.settingService = setting;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The footer.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Footer()
        {
            return this.PartialView(this.settingService.BlogName);
        }

        /// <summary>
        /// The get caption.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [HttpGet]
        public string GetCaption()
        {
            return this.settingService.BlogCaption;
        }

        /// <summary>
        /// The get theme.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [HttpGet]
        public string GetTheme()
        {
            return this.blogSettings.Theme.FindTheme(this.settingService);
        }

        /// <summary>
        /// The logo.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Logo()
        {
            var model = new LogoViewModel { BlogName = this.settingService.BlogName, RootUrl = this.GetRootUrl() };

            return this.PartialView(model);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get root url.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetRootUrl()
        {
            Uri url = this.Request.Url;
            if (url != null)
            {
                return string.Format("{0}://{1}{2}", url.Scheme, url.Authority, this.Url.Content("~"));
            }

            return "#";
        }

        #endregion
    }
}