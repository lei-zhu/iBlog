// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICategoryService.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The CategoryService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;

    using iBlog.Domain.Entities;

    /// <summary>
    /// The CategoryService interface.
    /// </summary>
    public interface ICategoryService : IDisposable
    {
        #region Public Methods and Operators

        /// <summary>
        /// The add category.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int AddCategory(CategoryEntity entity);

        /// <summary>
        /// The add category mapping.
        /// </summary>
        /// <param name="categoryEntity">
        /// The category entity.
        /// </param>
        /// <param name="postID">
        /// The post id.
        /// </param>
        void AddCategoryMapping(List<CategoryEntity> categoryEntity, int postID);

        /// <summary>
        /// The delete category.
        /// </summary>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        void DeleteCategory(int categoryID);

        /// <summary>
        /// The delete category mapping.
        /// </summary>
        /// <param name="postID">
        /// The post id.
        /// </param>
        void DeleteCategoryMapping(int postID);

        /// <summary>
        /// The delete category mapping.
        /// </summary>
        /// <param name="postIDList">
        /// The post id list.
        /// </param>
        void DeleteCategoryMapping(IEnumerable<int> postIDList);

        /// <summary>
        /// The get all categories.
        /// </summary>
        /// <returns>
        /// The <see cref="List{CategoryEntity}"/>.
        /// </returns>
        List<CategoryEntity> GetAllCategories();

        /// <summary>
        /// The get categories by post id.
        /// </summary>
        /// <param name="postID">
        /// The post id.
        /// </param>
        /// <returns>
        /// The <see cref="List{CategoryEntity}"/>.
        /// </returns>
        List<CategoryEntity> GetCategoriesByPostID(int postID);

        /// <summary>
        /// The update category by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="newCategoryName">
        /// The new category name.
        /// </param>
        void UpdateCategoryByID(int id, string newCategoryName);

        /// <summary>
        /// The update category mapping.
        /// </summary>
        /// <param name="categoryEntity">
        /// The category entity.
        /// </param>
        /// <param name="postID">
        /// The post id.
        /// </param>
        void UpdateCategoryMapping(List<CategoryEntity> categoryEntity, int postID);

        #endregion
    }
}