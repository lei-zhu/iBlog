// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommentService.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The comment service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Implements
{
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

    using iBlog.Domain.Entities;
    using iBlog.Domain.Interfaces;

    /// <summary>
    /// The comment service.
    /// </summary>
    public class CommentService : DisposableObject, ICommentService
    {
        #region Fields

        /// <summary>
        /// The comment table.
        /// </summary>
        private readonly Table<CommentEntity> commentTable;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentService"/> class.
        /// </summary>
        public CommentService()
        {
            this.commentTable = this.Context.GetTable<CommentEntity>();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="CommentService"/> class. 
        /// </summary>
        ~CommentService()
        {
            this.Dispose(false);
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add comment.
        /// </summary>
        /// <param name="commentEntity">
        /// The comment entity.
        /// </param>
        public void AddComment(CommentEntity commentEntity)
        {
            commentEntity.PostedTime = DateTime.Now;
            this.commentTable.InsertOnSubmit(commentEntity);
            this.Context.SubmitChanges();
        }

        /// <summary>
        /// The delete comment by comment id.
        /// </summary>
        /// <param name="commentID">
        /// The comment id.
        /// </param>
        public void DeleteCommentByCommentID(int commentID)
        {
            CommentEntity commentEntity = this.commentTable.SingleOrDefault(c => c.ID == commentID);
            if (commentEntity != null)
            {
                this.commentTable.DeleteOnSubmit(commentEntity);
                this.Context.SubmitChanges();
            }
        }

        /// <summary>
        /// The delete comments by post id.
        /// </summary>
        /// <param name="postID">
        /// The post id.
        /// </param>
        public void DeleteCommentsByPostID(int postID)
        {
            IQueryable<CommentEntity> comments = this.commentTable.Where(c => c.PostID == postID);
            if (comments.Any())
            {
                this.commentTable.DeleteAllOnSubmit(comments);
                this.Context.SubmitChanges();
            }
        }

        /// <summary>
        /// The delete comments by post id.
        /// </summary>
        /// <param name="postIDList">
        /// The post id list.
        /// </param>
        public void DeleteCommentsByPostID(IEnumerable<int> postIDList)
        {
            IQueryable<CommentEntity> comments = this.commentTable.Where(c => postIDList.Contains(c.PostID));
            if (comments.Any())
            {
                this.commentTable.DeleteAllOnSubmit(comments);
                this.Context.SubmitChanges();
            }
        }

        /// <summary>
        /// The get all comments.
        /// </summary>
        /// <returns>
        /// The <see cref="List{CommentEntity}"/>.
        /// </returns>
        public List<CommentEntity> GetAllComments()
        {
            return this.commentTable.OrderByDescending(c => c.PostedTime).ToList();
        }

        /// <summary>
        /// The get all comments.
        /// </summary>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <returns>
        /// The <see cref="List{CommentEntity}"/>.
        /// </returns>
        public List<CommentEntity> GetAllComments(int status)
        {
            return this.commentTable.Where(c => c.Status == status).OrderByDescending(c => c.PostedTime).ToList();
        }

        /// <summary>
        /// The get comments by post id.
        /// </summary>
        /// <param name="postID">
        /// The post id.
        /// </param>
        /// <returns>
        /// The <see cref="List{CommentEntity}"/>.
        /// </returns>
        public List<CommentEntity> GetCommentsByPostID(int postID)
        {
            return
                this.commentTable.Where(c => c.PostID == postID && (c.Status == 0 || c.Status == 1))
                    .OrderByDescending(c => c.PostedTime)
                    .ToList();
        }

        /// <summary>
        /// The update comment status.
        /// </summary>
        /// <param name="commentID">
        /// The comment id.
        /// </param>
        /// <param name="status">
        /// The status.
        /// </param>
        public void UpdateCommentStatus(int commentID, int status)
        {
            CommentEntity commentEntity = this.commentTable.SingleOrDefault(c => c.ID == commentID);
            if (commentEntity != null)
            {
                commentEntity.Status = status;
                this.Context.SubmitChanges();
            }
        }

        #endregion
    }
}