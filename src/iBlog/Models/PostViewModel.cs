// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostViewModel.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The post view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Models
{
    using System.Collections.Generic;

    using iBlog.Domain.Entities;

    /// <summary>
    /// The post view model.
    /// </summary>
    public class PostViewModel : PagedViewModel
    {
        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether any.
        /// </summary>
        public bool Any
        {
            get
            {
                return this.PostItems != null && this.PostItems.Count > 0;
            }
        }

        /// <summary>
        /// Gets or sets the author display name.
        /// </summary>
        public string AuthorDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the author name.
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Gets or sets the blog caption.
        /// </summary>
        public string BlogCaption { get; set; }

        /// <summary>
        /// Gets or sets the blog name.
        /// </summary>
        public string BlogName { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public CategoryEntity Category { get; set; }

        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        public string Month { get; set; }

        /// <summary>
        /// Gets or sets the post items.
        /// </summary>
        public List<PostItem> PostItems { get; set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        public TagEntity Tag { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        public string Year { get; set; }

        #endregion
    }

    /// <summary>
    /// The post item.
    /// </summary>
    public class PostItem
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the post.
        /// </summary>
        public PostEntity Post { get; set; }

        /// <summary>
        /// Gets or sets the root url.
        /// </summary>
        public string RootUrl { get; set; }

        #endregion
    }
}