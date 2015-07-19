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

    using iBlog.Domain.Entities;

    /// <summary>
    /// The recent controller.
    /// </summary>
    public class RecentController : HomeController
    {
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
            return null;
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
    }
}