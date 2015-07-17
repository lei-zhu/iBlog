// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TagMappingEntity.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The tag mapping entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Entities
{
    using System.Data.Linq.Mapping;

    /// <summary>
    /// The tag mapping entity.
    /// </summary>
    [Table(Name = "TagMapping")]
    public class TagMappingEntity
    {
        #region Public Properties

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

        /// <summary>
        /// Gets or sets the tag id.
        /// </summary>
        [Column]
        public int TagID { get; set; }

        #endregion
    }
}