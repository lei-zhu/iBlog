// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TagService.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The tag service.
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
    /// The tag service.
    /// </summary>
    public class TagService : DisposableObject, ITagService
    {
        #region Fields

        /// <summary>
        /// The tag mapping table.
        /// </summary>
        private readonly Table<TagMappingEntity> tagMappingTable;

        /// <summary>
        /// The tag table.
        /// </summary>
        private readonly Table<TagEntity> tagTable;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TagService"/> class.
        /// </summary>
        public TagService()
        {
            this.tagTable = this.Context.GetTable<TagEntity>();
            this.tagMappingTable = this.Context.GetTable<TagMappingEntity>();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="TagService"/> class. 
        /// </summary>
        ~TagService()
        {
            this.Dispose(false);
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add tags.
        /// </summary>
        /// <param name="tags">
        /// The tags.
        /// </param>
        public void AddTags(List<TagEntity> tags)
        {
            if (tags.Any())
            {
                this.tagTable.InsertAllOnSubmit(tags);
                this.Context.SubmitChanges();
            }
        }

        /// <summary>
        /// The add tags for post.
        /// </summary>
        /// <param name="tags">
        /// The tags.
        /// </param>
        /// <param name="postID">
        /// The post id.
        /// </param>
        public void AddTagsForPost(List<TagEntity> tags, int postID)
        {
            var tagMappings = new List<TagMappingEntity>();
            tags.ForEach(t => tagMappings.Add(new TagMappingEntity { TagID = t.ID, PostID = postID }));
            this.tagMappingTable.InsertAllOnSubmit(tagMappings);
            this.Context.SubmitChanges();
        }

        /// <summary>
        /// The delete tag.
        /// </summary>
        /// <param name="tagID">
        /// The tag id.
        /// </param>
        public void DeleteTag(int tagID)
        {
            IEnumerable<TagMappingEntity> currentMappings = this.tagMappingTable.Where(t => t.TagID == tagID);
            if (currentMappings.Any())
            {
                this.tagMappingTable.DeleteAllOnSubmit(currentMappings);
                this.Context.SubmitChanges();
            }

            TagEntity tagEntity = this.tagTable.SingleOrDefault(t => t.ID == tagID);
            if (tagEntity != null)
            {
                this.tagTable.DeleteOnSubmit(tagEntity);
                this.Context.SubmitChanges();
            }
        }

        /// <summary>
        /// The delete tags for post.
        /// </summary>
        /// <param name="postID">
        /// The post id.
        /// </param>
        public void DeleteTagsForPost(int postID)
        {
            List<TagMappingEntity> tagMappings = this.tagMappingTable.Where(t => t.PostID == postID).ToList();
            if (tagMappings.Count > 0)
            {
                this.tagMappingTable.DeleteAllOnSubmit(tagMappings);
                this.Context.SubmitChanges();
            }
        }

        /// <summary>
        /// The delete tags for posts.
        /// </summary>
        /// <param name="postIDList">
        /// The post id list.
        /// </param>
        public void DeleteTagsForPosts(IEnumerable<int> postIDList)
        {
            IQueryable<TagMappingEntity> tagMappings = this.tagMappingTable.Where(t => postIDList.Contains(t.PostID));
            if (tagMappings.Any())
            {
                this.tagMappingTable.DeleteAllOnSubmit(tagMappings);
                this.Context.SubmitChanges();
            }
        }

        /// <summary>
        /// The get all tags.
        /// </summary>
        /// <returns>
        /// The <see cref="List{TagEntity}"/>.
        /// </returns>
        public List<TagEntity> GetAllTags()
        {
            return this.tagTable.ToList();
        }

        /// <summary>
        /// The get tag by post id.
        /// </summary>
        /// <param name="postID">
        /// The post id.
        /// </param>
        /// <returns>
        /// The <see cref="List{TagEntity}"/>.
        /// </returns>
        public List<TagEntity> GetTagByPostID(int postID)
        {
            var tagEntities = new List<TagEntity>();
            List<TagEntity> allTags = this.GetAllTags();
            List<TagMappingEntity> tagsForPost = this.tagMappingTable.Where(t => t.PostID == postID).ToList();

            tagsForPost.ForEach(
                mapping =>
                    {
                        TagEntity tag = allTags.Single(t => t.ID == mapping.TagID);
                        var tagEntity = new TagEntity { ID = mapping.TagID, Name = tag.Name, Slug = tag.Slug };
                        tagEntities.Add(tagEntity);
                    });

            return tagEntities;
        }

        /// <summary>
        /// The update tags for post.
        /// </summary>
        /// <param name="tags">
        /// The tags.
        /// </param>
        /// <param name="postID">
        /// The post id.
        /// </param>
        public void UpdateTagsForPost(List<TagEntity> tags, int postID)
        {
            var tagMappings = new List<TagMappingEntity>();
            
            var postTags = this.tagMappingTable.Where(p => p.PostID == postID).ToList();
            tags.ForEach(t => tagMappings.Add(new TagMappingEntity { ID = t.ID, PostID = postID }));
            
            this.tagMappingTable.DeleteAllOnSubmit(postTags);
            this.tagMappingTable.InsertAllOnSubmit(tagMappings);

            this.Context.SubmitChanges();
        }

        #endregion
    }
}