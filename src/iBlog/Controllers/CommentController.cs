// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommentController.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The comment controller.
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
    using iBlog.Models;
    using iBlog.Service;

    /// <summary>
    /// The comment controller.
    /// </summary>
    public class CommentController : HomeController
    {
        #region Fields

        /// <summary>
        /// The cache service.
        /// </summary>
        private readonly ICacheService cacheService;

        /// <summary>
        /// The comment service.
        /// </summary>
        private readonly ICommentService commentService;

        /// <summary>
        /// The post service.
        /// </summary>
        private readonly IPostService postService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentController"/> class.
        /// </summary>
        public CommentController()
        {
            this.cacheService = ServiceLocator.Instance.GetService<ICacheService>();
            this.commentService = ServiceLocator.Instance.GetService<ICommentService>();
            this.postService = ServiceLocator.Instance.GetService<IPostService>();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="viewModel">
        /// The view model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Add(CommentViewModel viewModel)
        {
            var commentStatus = false;

            if (ModelState.IsValid)
            {
                if (viewModel.DisplayName == null || Request.IsAuthenticated)
                {
                    viewModel.Comment.PostID = viewModel.Post.ID;
                    if (Request.IsAuthenticated)
                    {
                        viewModel.Comment.UserID = this.GetUserId();
                    }

                    this.commentService.AddComment(viewModel.Comment);
                    commentStatus = true;
                }
            }

            return this.RedirectToPostPage(viewModel, commentStatus);
        }

        /// <summary>
        /// The recent comments.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        [ChildActionOnly]
        public ActionResult RecentComments()
        {
            List<RecentComment> comments = this.GetRecentComments();
            return this.PartialView("RecentComments", comments);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get recent comments.
        /// </summary>
        /// <returns>
        /// The <see cref="List{RecentComment}"/>.
        /// </returns>
        private List<RecentComment> GetRecentComments()
        {
            var recentComments = new List<RecentComment>();

            List<PostEntity> left = this.postService.GetAllPosts().Concat(this.postService.GetAllPages()).ToList();
            List<PostEntity> right =
                this.cacheService.GetPostsFromCache(this.postService, PostCacheUnauthKey, this.IsMarkDown())
                    .Concat(
                        this.cacheService.GetPagesFromCache(this.postService, PageCacheUnauthKey, this.IsMarkDown()))
                    .ToList();

            List<PostEntity> allPosts = this.Request.IsAuthenticated ? left : right;
            List<CommentEntity> topComments =
                allPosts.SelectMany(p => p.Comments)
                    .Where(c => c.Status == 0)
                    .OrderByDescending(c => c.PostedTime)
                    .Take(5)
                    .ToList();

            topComments.ForEach(
                comment =>
                    {
                        PostEntity post = allPosts.Single(p => p.ID == comment.PostID);
                        recentComments.Add(
                            new RecentComment
                                {
                                    Content = comment.Content, 
                                    PostedTime = post.CreateTime, 
                                    PostUrl = post.Url, 
                                    EntryType = post.EntryType
                                });
                    });

            return recentComments;
        }

        /// <summary>
        /// The redirect to post page.
        /// </summary>
        /// <param name="viewModel">
        /// The view model.
        /// </param>
        /// <param name="commentingStatus">
        /// The commenting status.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        private ActionResult RedirectToPostPage(CommentViewModel viewModel, bool commentingStatus)
        {
            var commentStatus = commentingStatus ? "comment-successed" : "comment-errored";
            if (viewModel.Post.EntryType == 1)
            {
                return this.RedirectToRoute(
                    "Post",
                    new
                        {
                            year = viewModel.Post.CreateTime.Year,
                            month = viewModel.Post.CreateTime.Month.ToString("00"),
                            url = viewModel.Post.Url,
                            status = commentStatus
                        });
            }

            return this.RedirectToRoute("Default", new { url = viewModel.Post.Url, status = commentStatus });
        }

        #endregion
    }
}