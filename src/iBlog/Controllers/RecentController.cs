// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecentController.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The recent controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using iBlog.Application;
    using iBlog.Collections;
    using iBlog.Domain.Entities;
    using iBlog.Domain.Interfaces;
    using iBlog.Service;

    /// <summary>
    /// The recent controller.
    /// </summary>
    public class RecentController : HomeController
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

        #endregion
        
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RecentController"/> class.
        /// </summary>
        public RecentController()
        {
            this.cacheService = ServiceLocator.Instance.GetService<ICacheService>();
            this.postService = ServiceLocator.Instance.GetService<IPostService>();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The archives.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        [ChildActionOnly]
        public ActionResult Archives()
        {
            var model = new ArchiveCollection(this.GetPosts());
            return this.PartialView(model);
        }

        /// <summary>
        /// The recent posts.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        [ChildActionOnly]
        public ActionResult RecentPosts()
        {
            List<PostEntity> posts = this.GetPosts();
            IEnumerable<PostEntity> model = posts.OrderByDescending(p => p.LastModifiedTime).Take(5);

            return this.PartialView("RecentPosts", model);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get posts.
        /// </summary>
        /// <returns>
        /// The <see cref="List{PostEntity}"/>.
        /// </returns>
        private new List<PostEntity> GetPosts()
        {
            var posts = Request.IsAuthenticated
                            ? this.postService.GetPosts(GetUserId())
                            : this.cacheService.GetPostsFromCache(this.postService, PostCacheUnauthKey, IsMarkDown());
            return posts;
        }

        #endregion
    }
}