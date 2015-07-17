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
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// The post entity.
    /// </summary>
    public partial class PostEntity
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        public List<CategoryEntity> Categories { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        public List<CommentEntity> Comments { get; set; }

        /// <summary>
        /// Gets the item type.
        /// </summary>
        public string ItemType
        {
            get
            {
                return this.EntryType == 1 ? "post" : "page";
            }
        }

        /// <summary>
        /// Gets the post month.
        /// </summary>
        public string PostMonth
        {
            get
            {
                return this.CreateTime.Month.ToString("00");
            }
        }

        /// <summary>
        /// Gets the post year.
        /// </summary>
        public string PostYear
        {
            get
            {
                return this.CreateTime.Year.ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        public List<TagEntity> Tags { get; set; }

        /// <summary>
        /// Gets or sets the user display name.
        /// </summary>
        public string UserDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        #endregion
    }
}