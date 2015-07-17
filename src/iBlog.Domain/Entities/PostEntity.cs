// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostEntity.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The post entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Entities
{
    using System;
    using System.ComponentModel;
    using System.Data.Linq.Mapping;

    /// <summary>
    /// The post entity.
    /// </summary>
    [Table(Name = "Posts")]
    public partial class PostEntity
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether can be shared.
        /// </summary>
        [Column]
        [DisplayName("允许分享文章？")]
        public bool CanBeShared { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        [Column]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the create time.
        /// </summary>
        [Column]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Gets or sets the entry type. /* 1 - 文章, 2 - 页面 */
        /// </summary>
        [Column]
        public byte EntryType { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is private.
        /// </summary>
        [Column]
        [DisplayName("私有文章")]
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Gets or sets the last modified time.
        /// </summary>
        [Column]
        public DateTime? LastModifiedTime { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        [Column]
        public int? Order { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [Column]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        [Column]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether user can add comments.
        /// </summary>
        [Column]
        [DisplayName("允许评论文章？")]
        public bool UserCanAddComments { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        [Column]
        public int UserID { get; set; }

        #endregion
    }
}