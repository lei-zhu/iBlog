// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITagService.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The TagService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;

    using iBlog.Domain.Entities;

    /// <summary>
    /// The TagService interface.
    /// </summary>
    public interface ITagService : IDisposable
    {
        #region Public Methods and Operators

        /// <summary>
        /// The add tags.
        /// </summary>
        /// <param name="tags">
        /// The tags.
        /// </param>
        void AddTags(List<TagEntity> tags);

        /// <summary>
        /// The add tags for post.
        /// </summary>
        /// <param name="tags">
        /// The tags.
        /// </param>
        /// <param name="postID">
        /// The post id.
        /// </param>
        void AddTagsForPost(List<TagEntity> tags, int postID);

        /// <summary>
        /// The delete tag.
        /// </summary>
        /// <param name="tagID">
        /// The tag id.
        /// </param>
        void DeleteTag(int tagID);

        /// <summary>
        /// The delete tags for post.
        /// </summary>
        /// <param name="postID">
        /// The post id.
        /// </param>
        void DeleteTagsForPost(int postID);

        /// <summary>
        /// The delete tags for posts.
        /// </summary>
        /// <param name="postIDList">
        /// The post id list.
        /// </param>
        void DeleteTagsForPosts(IEnumerable<int> postIDList);

        /// <summary>
        /// The get all tags.
        /// </summary>
        /// <returns>
        /// The <see cref="List{TagEntity}"/>.
        /// </returns>
        List<TagEntity> GetAllTags();

        /// <summary>
        /// The get tag by post id.
        /// </summary>
        /// <param name="postID">
        /// The post id.
        /// </param>
        /// <returns>
        /// The <see cref="List{TagEntity}"/>.
        /// </returns>
        List<TagEntity> GetTagByPostID(int postID);

        /// <summary>
        /// The update tags for post.
        /// </summary>
        /// <param name="tags">
        /// The tags.
        /// </param>
        /// <param name="postID">
        /// The post id.
        /// </param>
        void UpdateTagsForPost(List<TagEntity> tags, int postID);

        #endregion
    }
}