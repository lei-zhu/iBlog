// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryController.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The category controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// The category controller.
    /// </summary>
    public class CategoryController : Controller
    {
        [HttpGet]
        [ChildActionOnly]
        public ActionResult Categories()
        {
            return null;
        }
    }
}