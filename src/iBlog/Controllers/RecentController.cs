﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecentController.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The recent controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// The recent controller.
    /// </summary>
    public class RecentController : Controller
    {
        [HttpGet]
        [ChildActionOnly]
        public ActionResult RecentPosts()
        {
            return null;
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Archives()
        {
            return null;
        }
    }
}