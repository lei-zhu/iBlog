// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPostService.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The PostService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;

    using iBlog.Domain.Entities;

    /// <summary>
    /// The PostService interface.
    /// </summary>
    public interface IPostService : IDisposable
    {
        #region Public Methods and Operators

        /// <summary>
        /// The add post.
        /// </summary>
        /// <param name="postEntity">
        /// The post entity.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int AddPost(PostEntity postEntity);

        /// <summary>
        /// The delete post.
        /// </summary>
        /// <param name="postID">
        /// The post id.
        /// </param>
        void DeletePost(int postID);

        /// <summary>
        /// The delete posts by user id.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        void DeletePostsByUserID(int userID);

        /// <summary>
        /// The get all posts.
        /// </summary>
        /// <returns>
        /// The <see cref="List{PostEntity}"/>.
        /// </returns>
        List<PostEntity> GetAllPosts();

        /// <summary>
        /// The get all posts or pages.
        /// </summary>
        /// <param name="includeAll">
        /// The include all.
        /// </param>
        /// <returns>
        /// The <see cref="List{PostEntity}"/>.
        /// </returns>
        List<PostEntity> GetAllPostsOrPages(bool includeAll);

        /// <summary>
        /// The get pages.
        /// </summary>
        /// <returns>
        /// The <see cref="List{PostEntity}"/>.
        /// </returns>
        List<PostEntity> GetPages();

        /// <summary>
        /// The get pages.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="List{PostEntity}"/>.
        /// </returns>
        List<PostEntity> GetPages(int userID);

        /// <summary>
        /// The get post by id.
        /// </summary>
        /// <param name="postID">
        /// The post id.
        /// </param>
        /// <returns>
        /// The <see cref="PostEntity"/>.
        /// </returns>
        PostEntity GetPostByID(int postID);

        /// <summary>
        /// The get post by url.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="entryType">
        /// The entry type.
        /// </param>
        /// <returns>
        /// The <see cref="PostEntity"/>.
        /// </returns>
        PostEntity GetPostByUrl(string url, byte entryType);

        /// <summary>
        /// The get posts.
        /// </summary>
        /// <param name="excludeUserID">
        /// The exclude user id.
        /// </param>
        /// <param name="entryType">
        /// The entry type.
        /// </param>
        /// <returns>
        /// The <see cref="List{PostEntity}"/>.
        /// </returns>
        List<PostEntity> GetPosts(int excludeUserID, byte entryType);

        /// <summary>
        /// The get posts.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="List{PostEntity}"/>.
        /// </returns>
        List<PostEntity> GetPosts(int userID);

        /// <summary>
        /// The get posts by user id.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="List{PostEntity}"/>.
        /// </returns>
        List<PostEntity> GetPostsByUserID(int userID);

        /// <summary>
        /// The get posts by user id.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <param name="entryType">
        /// The entry type.
        /// </param>
        /// <returns>
        /// The <see cref="List{PostEntity}"/>.
        /// </returns>
        List<PostEntity> GetPostsByUserID(int userID, byte entryType);

        /// <summary>
        /// The update post.
        /// </summary>
        /// <param name="postEntity">
        /// The post entity.
        /// </param>
        void UpdatePost(PostEntity postEntity);

        #endregion
    }
}