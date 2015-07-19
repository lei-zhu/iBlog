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
    using System.Linq;
    using System.Web.Mvc;

    using iBlog.Application;
    using iBlog.Domain.Entities;
    using iBlog.Domain.Interfaces;
    using iBlog.Exceptions;
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

        /// <summary>
        /// The user service.
        /// </summary>
        private readonly IUserService userService;

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
            this.userService = ServiceLocator.Instance.GetService<IUserService>();
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
        [HttpGet]
        public ActionResult Index(int? page)
        {
            List<PostEntity> posts = this.GetPosts();
            PostViewModel viewModel = posts.GetPostViewModel(page, this.settingService, this.GetRootUrl());
            return this.View(viewModel);
        }

        /// <summary>
        /// The post page.
        /// </summary>
        /// <param name="year">
        /// The year.
        /// </param>
        /// <param name="month">
        /// The month.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult PostPage(string year, string month, string url, string status)
        {
            var allPosts = this.GetPosts();
            var current = allPosts.SingleOrDefault(p => p.Url == url && p.EntryType == 1);
            if (current == null)
            {
                throw new UrlNotFoundException("Unable to find a post w/ the url {0} for the month {1} and year {2}", url, month, year);
            }

            if (!Request.IsAuthenticated && status == "comment-posted")
            {
                var recentPost = this.postService.GetPostByUrl(url, 1);
                current.Comments = recentPost.Comments;
            }

            var index = allPosts.IndexOf(current);
            var model = new PostPageViewModel
            {
                Post = current,
                PreviousPost = index == 0 || index < 0 ? null : allPosts[index - 1],
                NextPost = index == (allPosts.Count - 1) || index < 0 ? null : allPosts[index + 1],
                UserCanEdit = Request.IsAuthenticated && (current.UserID == GetUserId() || User.IsInRole("SuperAdmin")),
                BlogName = this.settingService.BlogName,
                BlogCaption = this.settingService.BlogCaption,
                CommentEntity = this.GetCommentEntity()
            };

            return this.View(model);
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

        /// <summary>
        /// The get comment entity.
        /// </summary>
        /// <returns>
        /// The <see cref="CommentEntity"/>.
        /// </returns>
        private CommentEntity GetCommentEntity()
        {
            var userId = GetUserId();
            if (userId == -1)
            {
                return new CommentEntity();
            }

            var user = this.userService.GetUserByUserID(userId);
            return new CommentEntity
            {
                CommenterName = user.DisplayName,
                CommenterEmail = user.EmailAddress,
                CommenterSite = user.UserSite
            };
        }

        #endregion
    }
}