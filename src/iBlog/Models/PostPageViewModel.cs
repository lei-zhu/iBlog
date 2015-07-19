// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostPageViewModel.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The post page view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Models
{
    using iBlog.Domain.Entities;

    /// <summary>
    /// The post page view model.
    /// </summary>
    public class PostPageViewModel
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the blog caption.
        /// </summary>
        public string BlogCaption { get; set; }

        /// <summary>
        /// Gets or sets the blog name.
        /// </summary>
        public string BlogName { get; set; }

        /// <summary>
        /// Gets or sets the comment entity.
        /// </summary>
        public CommentEntity CommentEntity { get; set; }

        /// <summary>
        /// Gets or sets the next post.
        /// </summary>
        public PostEntity NextPost { get; set; }

        /// <summary>
        /// Gets or sets the post.
        /// </summary>
        public PostEntity Post { get; set; }

        /// <summary>
        /// Gets or sets the previous post.
        /// </summary>
        public PostEntity PreviousPost { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether user can edit.
        /// </summary>
        public bool UserCanEdit { get; set; }

        #endregion
    }
}