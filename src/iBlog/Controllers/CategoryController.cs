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
    using System.Collections.Generic;
    using System.Web.Mvc;

    using iBlog.Domain.Entities;
    using iBlog.Domain.Interfaces;
    using iBlog.Service;

    /// <summary>
    /// The category controller.
    /// </summary>
    public class CategoryController : Controller
    {
        #region Fields

        /// <summary>
        /// The category service.
        /// </summary>
        private readonly ICategoryService categoryService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController"/> class.
        /// </summary>
        public CategoryController()
        {
            this.categoryService = ServiceLocator.Instance.GetService<ICategoryService>();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The categories.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        [ChildActionOnly]
        public ActionResult Categories()
        {
            List<CategoryEntity> model = this.categoryService.GetAllCategories();
            return this.PartialView("Categories", model);
        }

        #endregion
    }
}