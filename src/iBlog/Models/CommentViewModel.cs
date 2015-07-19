// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommentViewModel.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The comment view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Models
{
    using System;

    using iBlog.Domain.Entities;

    /// <summary>
    /// The comment view model.
    /// </summary>
    public class CommentViewModel
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        public CommentEntity Comment { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the is human.
        /// </summary>
        public string IsHuman { get; set; }

        /// <summary>
        /// Gets or sets the post.
        /// </summary>
        public PostEntity Post { get; set; }

        #endregion
    }

    /// <summary>
    /// The recent comment.
    /// </summary>
    public class RecentComment
    {
        #region Fields

        /// <summary>
        /// The content.
        /// </summary>
        private string content;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public string Content
        {
            get
            {
                return this.content.Length > 25 ? this.content.Substring(0, 25) + "..." : this.content;
            }

            set
            {
                this.content = value;
            }
        }

        /// <summary>
        /// Gets or sets the entry type.
        /// </summary>
        public byte EntryType { get; set; }

        /// <summary>
        /// Gets or sets the post url.
        /// </summary>
        public string PostUrl { get; set; }

        /// <summary>
        /// Gets or sets the posted time.
        /// </summary>
        public DateTime PostedTime { get; set; }

        #endregion
    }
}