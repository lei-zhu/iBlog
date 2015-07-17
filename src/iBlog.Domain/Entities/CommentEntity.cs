// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommentEntity.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The comment entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Entities
{
    using System;
    using System.Data.Linq.Mapping;

    /// <summary>
    /// The comment entity.
    /// </summary>
    [Table(Name = "Comments")]
    public class CommentEntity
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the commenter email.
        /// </summary>
        [Column]
        public string CommenterEmail { get; set; }

        /// <summary>
        /// Gets or sets the commenter name.
        /// </summary>
        [Column]
        public string CommenterName { get; set; }

        /// <summary>
        /// Gets or sets the commenter site.
        /// </summary>
        [Column]
        public string CommenterSite { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        [Column]
        public string Content { get; set; }

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
        /// Gets or sets the posted time.
        /// </summary>
        [Column]
        public DateTime PostedTime { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [Column]
        public int Status { get; set; } /* 0 - 审核通过, 1 - 等待审核, 2 - 垃圾评论, -1 - 已删除 */

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        [Column]
        public int? UserID { get; set; }

        #endregion
    }
}