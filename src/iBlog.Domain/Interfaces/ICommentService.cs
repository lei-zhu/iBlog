// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommentService.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The CommentService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;

    using iBlog.Domain.Entities;

    /// <summary>
    /// The CommentService interface.
    /// </summary>
    public interface ICommentService : IDisposable
    {
        #region Public Methods and Operators

        /// <summary>
        /// The add comment.
        /// </summary>
        /// <param name="commentEntity">
        /// The comment entity.
        /// </param>
        void AddComment(CommentEntity commentEntity);

        /// <summary>
        /// The delete comment by comment id.
        /// </summary>
        /// <param name="commentID">
        /// The comment id.
        /// </param>
        void DeleteCommentByCommentID(int commentID);

        /// <summary>
        /// The delete comments by post id.
        /// </summary>
        /// <param name="postID">
        /// The post id.
        /// </param>
        void DeleteCommentsByPostID(int postID);

        /// <summary>
        /// The delete comments by post id.
        /// </summary>
        /// <param name="postIDList">
        /// The post id list.
        /// </param>
        void DeleteCommentsByPostID(IEnumerable<int> postIDList);

        /// <summary>
        /// The get all comments.
        /// </summary>
        /// <returns>
        /// The <see cref="List{CommentEntity}"/>.
        /// </returns>
        List<CommentEntity> GetAllComments();

        /// <summary>
        /// The get all comments.
        /// </summary>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <returns>
        /// The <see cref="List{CommentEntity}"/>.
        /// </returns>
        List<CommentEntity> GetAllComments(int status);

        /// <summary>
        /// The get comments by post id.
        /// </summary>
        /// <param name="postID">
        /// The post id.
        /// </param>
        /// <returns>
        /// The <see cref="List{CommentEntity}"/>.
        /// </returns>
        List<CommentEntity> GetCommentsByPostID(int postID);

        /// <summary>
        /// The update comment status.
        /// </summary>
        /// <param name="commentID">
        /// The comment id.
        /// </param>
        /// <param name="status">
        /// The status.
        /// </param>
        void UpdateCommentStatus(int commentID, int status);

        #endregion
    }
}