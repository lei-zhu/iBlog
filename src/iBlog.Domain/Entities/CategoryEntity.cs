// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryEntity.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The category entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Entities
{
    using System.Data.Linq.Mapping;

    /// <summary>
    /// The category entity.
    /// </summary>
    [Table(Name = "Categories")]
    public class CategoryEntity
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Column]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        [Column]
        public string Slug { get; set; }

        #endregion
    }
}