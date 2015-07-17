// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryMappingEntity.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The category mapping entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Entities
{
    using System.Data.Linq.Mapping;

    /// <summary>
    /// The category mapping entity.
    /// </summary>
    [Table(Name = "CategoryMapping")]
    public class CategoryMappingEntity
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        [Column]
        public int CategoryID { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the post id.
        /// </summary>
        [Column]
        public int PostID { get; set; }

        #endregion
    }
}