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
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    using iBlog.Application;
    using iBlog.Configuration;
    using iBlog.Domain.Entities;
    using iBlog.Domain.Interfaces;
    using iBlog.Models;
    using iBlog.Service;

    /// <summary>
    ///     The blog controller.
    /// </summary>
    public class BlogController : Controller
    {
        #region Fields

        /// <summary>
        /// The post cache unauth key.
        /// </summary>
        protected const string PostCacheUnauthKey = "GetAllPosts";

        /// <summary>
        /// The page cache unauth key.
        /// </summary>
        protected const string PageCacheUnauthKey = "GetAllPages";

        /// <summary>
        /// The blog settings.
        /// </summary>
        private readonly SettingConfigSection blogSettings =
            ConfigurationManager.GetSection("iBlogSettings") as SettingConfigSection;

        /// <summary>
        /// The cache service.
        /// </summary>
        private readonly ICacheService cacheService;

        /// <summary>
        /// The post service.
        /// </summary>
        private readonly IPostService postService;

        /// <summary>
        /// The setting service.
        /// </summary>
        private readonly ISettingService settingService;

        #endregion
        
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogController"/> class.
        /// </summary>
        public BlogController()
        {
            this.cacheService = ServiceLocator.Instance.GetService<ICacheService>();
            this.postService = ServiceLocator.Instance.GetService<IPostService>();
            this.settingService = ServiceLocator.Instance.GetService<ISettingService>();
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
            return this.PartialView("Footer", this.settingService.BlogName);
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
        /// The menu.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        [ChildActionOnly]
        public ActionResult Menu()
        {
            var pages = this.GetPages();
            pages = pages.Take(4).ToList();

            var url = this.Request.Url;
            if (url != null)
            {
                var pageName = GetPageName(url.ToString());
                return this.PartialView(this.GetMenuViewModel(pages, pageName));
            }

            return null;
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
        /// The get pages.
        /// </summary>
        /// <returns>
        /// The <see cref="List{PostEntity}"/>.
        /// </returns>
        protected List<PostEntity> GetPages()
        {
            var pages = Request.IsAuthenticated
                            ? MarkdownTransform(this.postService.GetAllPages(this.GetUserId()), this.IsMarkDown())
                            : this.cacheService.GetPagesFromCache(this.postService, PageCacheUnauthKey, this.IsMarkDown());

            return pages.OrderBy(p => p.Order != null ? p.Order.Value : 0).ToList();
        }

        /// <summary>
        /// The get root url.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        protected string GetRootUrl()
        {
            Uri url = this.Request.Url;
            if (url != null)
            {
                return string.Format("{0}://{1}{2}", url.Scheme, url.Authority, this.Url.Content("~"));
            }

            return "#";
        }

        /// <summary>
        /// The get user id.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected int GetUserId()
        {
            var userId = -1;
            if (Request.IsAuthenticated)
            {
                var userInfo = (IUserInfo)User.Identity;
                userId = int.Parse(userInfo.UserId);
            }

            return userId;
        }

        /// <summary>
        /// The is mark down.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected bool IsMarkDown()
        {
            return this.settingService.EditorType.ToLower() == "markdown";
        }

        /// <summary>
        /// The get page name.
        /// </summary>
        /// <param name="pageUrl">
        /// The page url.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetPageName(string pageUrl)
        {
            return Regex.Match(pageUrl, @"pages\/([^)]*)").Groups[1].Value;
        }

        /// <summary>
        /// The markdown transform.
        /// </summary>
        /// <param name="posts">
        /// The posts.
        /// </param>
        /// <param name="isMarkDown">
        /// The is mark down.
        /// </param>
        /// <returns>
        /// The <see cref="List{PostEntity}"/>.
        /// </returns>
        private static List<PostEntity> MarkdownTransform(List<PostEntity> posts, bool isMarkDown)
        {
            var markdown = new MarkdownDeep.Markdown { ExtraMode = true };
            if (isMarkDown)
            {
                posts.ForEach(p =>
                {
                    p.Content = markdown.Transform(p.Content);
                });
            }

            return posts;
        }

        /// <summary>
        /// The get menu view model.
        /// </summary>
        /// <param name="pages">
        /// The pages.
        /// </param>
        /// <param name="requestedPageName">
        /// The requested page name.
        /// </param>
        /// <returns>
        /// The <see cref="MenuViewModel"/>.
        /// </returns>
        private MenuViewModel GetMenuViewModel(List<PostEntity> pages, string requestedPageName)
        {
            var viewModel = new MenuViewModel();

            var menuItems = new List<MenuItem> { new MenuItem { Title = "home", Url = "/", Selected = false } };

            pages.ForEach(p => menuItems.Add(new MenuItem { Title = p.Title, Url = p.Url, Selected = p.Url == requestedPageName }));

            if (!menuItems.Any(p => p.Selected) && requestedPageName == string.Empty)
            {
                var home = menuItems.Single(p => p.Title == "home");
                home.Selected = true;
            }

            viewModel.MenuItems = menuItems;
            return viewModel;
        }

        #endregion
    }
}