// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostService.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The post service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Implements
{
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

    using iBlog.Domain.Entities;
    using iBlog.Domain.Interfaces;

    /// <summary>
    /// The post service.
    /// </summary>
    public class PostService : DisposableObject, IPostService
    {
        #region Fields

        /// <summary>
        /// The category service.
        /// </summary>
        private readonly ICategoryService categoryService;

        /// <summary>
        /// The comment service.
        /// </summary>
        private readonly ICommentService commentService;

        /// <summary>
        /// The post table.
        /// </summary>
        private readonly Table<PostEntity> postTable;

        /// <summary>
        /// The tag service.
        /// </summary>
        private readonly ITagService tagService;

        /// <summary>
        /// The user service.
        /// </summary>
        private readonly IUserService userService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PostService"/> class.
        /// </summary>
        /// <param name="userService">
        /// The user service.
        /// </param>
        /// <param name="categoryService">
        /// The category service.
        /// </param>
        /// <param name="tagService">
        /// The tag service.
        /// </param>
        /// <param name="commentService">
        /// The comment service.
        /// </param>
        public PostService(
            IUserService userService, 
            ICategoryService categoryService, 
            ITagService tagService, 
            ICommentService commentService)
        {
            this.postTable = this.Context.GetTable<PostEntity>();

            this.commentService = commentService;
            this.tagService = tagService;
            this.categoryService = categoryService;
            this.userService = userService;
        }

        #endregion

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
        public int AddPost(PostEntity postEntity)
        {
            int postID = -1;
            try
            {
                postID = this.AddPostInternal(postEntity);

                this.categoryService.AddCategoryMapping(postEntity.Categories, postID);

                if (postEntity.Tags != null)
                {
                    this.tagService.AddTagsForPost(postEntity.Tags, postID);
                }

                return postID;
            }
            catch
            {
                if (postID > 0)
                {
                    this.categoryService.DeleteCategoryMapping(postID);
                    this.tagService.DeleteTagsForPost(postID);
                }

                this.DeletePost(postID);

                return -1;
            }
        }

        /// <summary>
        /// The delete post.
        /// </summary>
        /// <param name="postID">
        /// The post id.
        /// </param>
        public void DeletePost(int postID)
        {
            PostEntity post = this.postTable.SingleOrDefault(p => p.ID == postID);

            if (post != null)
            {
                this.commentService.DeleteCommentsByPostID(postID);
                this.tagService.DeleteTagsForPost(postID);
                this.categoryService.DeleteCategoryMapping(postID);

                this.postTable.DeleteOnSubmit(post);

                this.Context.SubmitChanges();
            }
        }

        /// <summary>
        /// The delete posts by user id.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        public void DeletePostsByUserID(int userID)
        {
            IQueryable<PostEntity> posts = this.postTable.Where(p => p.UserID == userID);
            if (posts.Any())
            {
                this.postTable.DeleteAllOnSubmit(posts);
                this.Context.SubmitChanges();
            }
        }

        /// <summary>
        /// The get all posts.
        /// </summary>
        /// <returns>
        /// The <see cref="List{PostEntity}"/>.
        /// </returns>
        public List<PostEntity> GetAllPosts()
        {
            List<PostEntity> postEntities =
                this.postTable.Where(p => !p.IsPrivate && p.EntryType == 1)
                    .OrderByDescending(p => p.LastModifiedTime)
                    .ToList();
            return this.GetPostRelevantData(postEntities);
        }

        /// <summary>
        /// The get all posts or pages.
        /// </summary>
        /// <param name="includeAll">
        /// The include all.
        /// </param>
        /// <returns>
        /// The <see cref="List{PostEntity}"/>.
        /// </returns>
        public List<PostEntity> GetAllPostsOrPages(bool includeAll)
        {
            List<PostEntity> postEntities = this.postTable.ToList();
            return !includeAll ? postEntities : this.GetPostRelevantData(postEntities);
        }

        /// <summary>
        /// The get all pages.
        /// </summary>
        /// <returns>
        /// The <see cref="List{PostEntity}"/>.
        /// </returns>
        public List<PostEntity> GetAllPages()
        {
            List<PostEntity> postEntities = this.postTable.Where(p => !p.IsPrivate && p.EntryType == 2).ToList();
            return this.GetPostRelevantData(postEntities);
        }

        /// <summary>
        /// The get pages.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="List{PostEntity}"/>.
        /// </returns>
        public List<PostEntity> GetAllPages(int userID)
        {
            List<PostEntity> postEntities =
                this.postTable.Where(
                    p => (!p.IsPrivate && p.EntryType == 2) || (p.IsPrivate && p.EntryType == 2 && p.UserID == userID))
                    .ToList();
            return this.GetPostRelevantData(postEntities);
        }

        /// <summary>
        /// The get post by id.
        /// </summary>
        /// <param name="postID">
        /// The post id.
        /// </param>
        /// <returns>
        /// The <see cref="PostEntity"/>.
        /// </returns>
        public PostEntity GetPostByID(int postID)
        {
            PostEntity post = this.postTable.SingleOrDefault(p => p.ID == postID);
            if (post != null)
            {
                UserEntity user = this.userService.GetAllUsers().Single(u => u.ID == post.UserID);
                post.Comments = this.commentService.GetCommentsByPostID(post.ID);
                post.Categories = this.categoryService.GetCategoriesByPostID(post.ID);
                post.Tags = this.tagService.GetTagByPostID(post.ID);
                post.UserDisplayName = user.DisplayName;
                post.UserName = user.UserName;
            }

            return post;
        }

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
        public PostEntity GetPostByUrl(string url, byte entryType)
        {
            PostEntity post = this.postTable.SingleOrDefault(p => p.Url == url && p.EntryType == entryType);
            return post != null ? this.GetPostByID(post.ID) : null;
        }

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
        public List<PostEntity> GetPosts(int excludeUserID, byte entryType)
        {
            List<PostEntity> postEntities =
                this.postTable.Where(p => p.EntryType == entryType && p.UserID != excludeUserID && !p.IsPrivate)
                    .ToList();
            return this.GetPostRelevantData(postEntities);
        }

        /// <summary>
        /// The get posts.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="List{PostEntity}"/>.
        /// </returns>
        public List<PostEntity> GetPosts(int userID)
        {
            List<PostEntity> postEntities =
                this.postTable.Where(
                    p => (!p.IsPrivate && p.EntryType == 1) || (p.IsPrivate && p.EntryType == 1 && p.UserID == userID))
                    .OrderByDescending(p => p.LastModifiedTime)
                    .ToList();

            return this.GetPostRelevantData(postEntities);
        }

        /// <summary>
        /// The get posts by user id.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="List{PostEntity}"/>.
        /// </returns>
        public List<PostEntity> GetPostsByUserID(int userID)
        {
            List<PostEntity> postEntities =
                this.postTable.Where(p => p.UserID == userID).OrderByDescending(p => p.LastModifiedTime).ToList();
            return this.GetPostRelevantData(postEntities);
        }

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
        public List<PostEntity> GetPostsByUserID(int userID, byte entryType)
        {
            List<PostEntity> postEntities =
                this.postTable.Where(p => p.UserID == userID && p.EntryType == entryType)
                    .OrderByDescending(p => p.LastModifiedTime)
                    .ToList();
            return this.GetPostRelevantData(postEntities);
        }

        /// <summary>
        /// The update post.
        /// </summary>
        /// <param name="postEntity">
        /// The post entity.
        /// </param>
        public void UpdatePost(PostEntity postEntity)
        {
            try
            {
                this.UpdatePostInternal(postEntity);

                this.categoryService.UpdateCategoryMapping(postEntity.Categories, postEntity.ID);

                if (postEntity.Tags != null)
                {
                    this.tagService.UpdateTagsForPost(postEntity.Tags, postEntity.ID);
                }
            }
            catch
            {
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The add post internal.
        /// </summary>
        /// <param name="postEntity">
        /// The post entity.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private int AddPostInternal(PostEntity postEntity)
        {
            this.postTable.InsertOnSubmit(postEntity);
            this.Context.SubmitChanges();

            return postEntity.ID;
        }

        /// <summary>
        /// The get post relevant data.
        /// </summary>
        /// <param name="postEntities">
        /// The post entities.
        /// </param>
        /// <returns>
        /// The <see cref="List{PostEntity}"/>.
        /// </returns>
        private List<PostEntity> GetPostRelevantData(List<PostEntity> postEntities)
        {
            List<UserEntity> users = this.userService.GetAllUsers().ToList();
            postEntities.ForEach(
                p =>
                    {
                        p.Comments = this.commentService.GetCommentsByPostID(p.ID);
                        p.Categories = this.categoryService.GetCategoriesByPostID(p.ID);
                        p.Tags = this.tagService.GetTagByPostID(p.ID);

                        UserEntity user = users.Single(u => u.ID == p.UserID);
                        p.UserDisplayName = user.DisplayName;
                        p.UserName = user.UserName;
                    });

            return postEntities;
        }

        /// <summary>
        /// The update post internal.
        /// </summary>
        /// <param name="postEntity">
        /// The post entity.
        /// </param>
        private void UpdatePostInternal(PostEntity postEntity)
        {
            PostEntity post = this.postTable.SingleOrDefault(p => p.ID == postEntity.ID);
            if (post != null)
            {
                post.Title = postEntity.Title;
                post.Content = postEntity.Content;
                post.Url = postEntity.Url;
                post.LastModifiedTime = postEntity.LastModifiedTime;
                post.CanAddComments = postEntity.CanAddComments;
                post.CanBeShared = postEntity.CanBeShared;
                post.IsPrivate = postEntity.IsPrivate;
                post.EntryType = postEntity.EntryType;
                post.Order = postEntity.Order.HasValue ? postEntity.Order.Value : (int?)null;

                this.Context.SubmitChanges();
            }
        }

        #endregion
    }
}