// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryService.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The category service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Implements
{
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

    using iBlog.Domain.Entities;
    using iBlog.Domain.Interfaces;

    /// <summary>
    /// The category service.
    /// </summary>
    public class CategoryService : DisposableObject, ICategoryService
    {
        #region Fields

        /// <summary>
        /// The category mapping table.
        /// </summary>
        private readonly Table<CategoryMappingEntity> categoryMappingTable;

        /// <summary>
        /// The category table.
        /// </summary>
        private readonly Table<CategoryEntity> categoryTable;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryService"/> class.
        /// </summary>
        public CategoryService()
        {
            this.categoryTable = this.Context.GetTable<CategoryEntity>();
            this.categoryMappingTable = this.Context.GetTable<CategoryMappingEntity>();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="CategoryService"/> class. 
        /// </summary>
        ~CategoryService()
        {
            this.Dispose(false);
        }

        #endregion

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
        public int AddCategory(CategoryEntity entity)
        {
            if (entity != null)
            {
                this.categoryTable.InsertOnSubmit(entity);
                this.Context.SubmitChanges();

                return entity.ID;
            }

            return -1;
        }

        /// <summary>
        /// The add category mapping.
        /// </summary>
        /// <param name="categoryEntity">
        /// The category entity.
        /// </param>
        /// <param name="postID">
        /// The post id.
        /// </param>
        public void AddCategoryMapping(List<CategoryEntity> categoryEntity, int postID)
        {
            var postCategoryMappings = new List<CategoryMappingEntity>();

            categoryEntity.ForEach(
                c => postCategoryMappings.Add(new CategoryMappingEntity { CategoryID = c.ID, PostID = postID }));

            this.categoryMappingTable.InsertAllOnSubmit(postCategoryMappings);
            this.Context.SubmitChanges();
        }

        /// <summary>
        /// The delete category.
        /// </summary>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        public void DeleteCategory(int categoryID)
        {
            IEnumerable<CategoryMappingEntity> currentMappings =
                this.categoryMappingTable.Where(c => c.CategoryID == categoryID);
            if (currentMappings.Any())
            {
                this.categoryMappingTable.DeleteAllOnSubmit(currentMappings);
                this.Context.SubmitChanges();
            }

            CategoryEntity entity = this.categoryTable.SingleOrDefault(c => c.ID == categoryID);
            if (entity != null)
            {
                this.categoryTable.DeleteOnSubmit(entity);
                this.Context.SubmitChanges();
            }
        }

        /// <summary>
        /// The delete category mapping.
        /// </summary>
        /// <param name="postID">
        /// The post id.
        /// </param>
        public void DeleteCategoryMapping(int postID)
        {
            List<CategoryMappingEntity> mappings = this.categoryMappingTable.Where(m => m.PostID == postID).ToList();
            if (mappings.Count > 0)
            {
                this.categoryMappingTable.DeleteAllOnSubmit(mappings);
                this.Context.SubmitChanges();
            }
        }

        /// <summary>
        /// The delete category mapping.
        /// </summary>
        /// <param name="postIDList">
        /// The post id list.
        /// </param>
        public void DeleteCategoryMapping(IEnumerable<int> postIDList)
        {
            IQueryable<CategoryMappingEntity> mappings =
                this.categoryMappingTable.Where(c => postIDList.Contains(c.PostID));
            if (mappings.Any())
            {
                this.categoryMappingTable.DeleteAllOnSubmit(mappings);
                this.Context.SubmitChanges();
            }
        }

        /// <summary>
        /// The get all categories.
        /// </summary>
        /// <returns>
        /// The <see cref="List{CategoryEntity}"/>.
        /// </returns>
        public List<CategoryEntity> GetAllCategories()
        {
            return this.categoryTable.OrderBy(c => c.ID).ToList();
        }

        /// <summary>
        /// The get categories by post id.
        /// </summary>
        /// <param name="postID">
        /// The post id.
        /// </param>
        /// <returns>
        /// The <see cref="List{CategoryEntity}"/>.
        /// </returns>
        public List<CategoryEntity> GetCategoriesByPostID(int postID)
        {
            var categoriesEntities = new List<CategoryEntity>();
            List<CategoryEntity> allCategories = this.GetAllCategories();
            List<CategoryMappingEntity> postCategoryMappings =
                this.categoryMappingTable.Where(m => m.PostID == postID).ToList();

            postCategoryMappings.ForEach(
                mapping =>
                    {
                        CategoryEntity category = allCategories.Single(c => c.ID == mapping.CategoryID);
                        var categoryEntity = new CategoryEntity
                                                 {
                                                     ID = mapping.CategoryID, 
                                                     Name = category.Name, 
                                                     Slug = category.Slug
                                                 };
                        categoriesEntities.Add(categoryEntity);
                    });

            return categoriesEntities;
        }

        /// <summary>
        /// The update category by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="newCategoryName">
        /// The new category name.
        /// </param>
        public void UpdateCategoryByID(int id, string newCategoryName)
        {
            CategoryEntity categoryEntity = this.categoryTable.SingleOrDefault(c => c.ID == id);
            if (categoryEntity != null)
            {
                categoryEntity.Name = newCategoryName;
                this.Context.SubmitChanges();
            }
        }

        /// <summary>
        /// The update category mapping.
        /// </summary>
        /// <param name="categoryEntity">
        /// The category entity.
        /// </param>
        /// <param name="postID">
        /// The post id.
        /// </param>
        public void UpdateCategoryMapping(List<CategoryEntity> categoryEntity, int postID)
        {
            var postCategoryMappings = new List<CategoryMappingEntity>();

            List<CategoryMappingEntity> postMappings = this.categoryMappingTable.Where(p => p.PostID == postID).ToList();
            categoryEntity.ForEach(
                c => postCategoryMappings.Add(new CategoryMappingEntity { CategoryID = c.ID, PostID = postID }));

            this.categoryMappingTable.DeleteAllOnSubmit(postMappings);
            this.categoryMappingTable.InsertAllOnSubmit(postCategoryMappings);

            this.Context.SubmitChanges();
        }

        #endregion
    }
}