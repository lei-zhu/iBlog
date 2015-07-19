// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The home controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using iBlog.Application;
    using iBlog.Domain.Entities;
    using iBlog.Domain.Interfaces;
    using iBlog.Models;
    using iBlog.Service;

    using MarkdownDeep;

    /// <summary>
    /// The home controller.
    /// </summary>
    public class HomeController : BlogController
    {
        #region Fields

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
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        public HomeController()
        {
            this.cacheService = ServiceLocator.Instance.GetService<ICacheService>();
            this.settingService = ServiceLocator.Instance.GetService<ISettingService>();
            this.postService = ServiceLocator.Instance.GetService<IPostService>();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index(int? page)
        {
            List<PostEntity> posts = this.GetPosts();
            PostViewModel viewModel = posts.GetPostViewModel(page, this.settingService, this.GetRootUrl());
            return this.View(viewModel);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get posts.
        /// </summary>
        /// <returns>
        /// The <see cref="List{PostEntity}"/>.
        /// </returns>
        protected List<PostEntity> GetPosts()
        {
            List<PostEntity> posts = this.Request.IsAuthenticated
                                         ? MarkdownTransform(
                                             this.postService.GetPosts(this.GetUserId()),
                                             this.IsMarkDown())
                                         : this.cacheService.GetPostsFromCache(
                                             this.postService,
                                             PostCacheUnauthKey,
                                             this.IsMarkDown());
            return posts;
        }

        /// <summary>
        /// The markdown transform.
        /// </summary>
        /// <param name="postList">
        /// The post list.
        /// </param>
        /// <param name="isMarkDown">
        /// The is mark down.
        /// </param>
        /// <returns>
        /// The <see cref="List{PostEntity}"/>.
        /// </returns>
        private static List<PostEntity> MarkdownTransform(List<PostEntity> postList, bool isMarkDown)
        {
            var markdown = new Markdown { ExtraMode = true };
            postList.ForEach(
                p =>
                    {
                        if (isMarkDown)
                        {
                            p.Content = markdown.Transform(p.Content);
                        }

                        if (p.IsPrivate)
                        {
                            p.Title = string.Format("[Private] {0}", p.Title);
                        }
                    });

            return postList;
        }

        #endregion
    }
}