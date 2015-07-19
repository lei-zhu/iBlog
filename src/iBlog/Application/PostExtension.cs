// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostExtension.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The post extension.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using iBlog.Domain.Entities;
    using iBlog.Domain.Interfaces;
    using iBlog.Models;

    /// <summary>
    /// The post extension.
    /// </summary>
    public static class PostExtension
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get post view model.
        /// </summary>
        /// <param name="posts">
        /// The posts.
        /// </param>
        /// <param name="pageNumber">
        /// The page number.
        /// </param>
        /// <param name="settingService">
        /// The setting service.
        /// </param>
        /// <param name="rootUrl">
        /// The root url.
        /// </param>
        /// <returns>
        /// The <see cref="PostViewModel"/>.
        /// </returns>
        public static PostViewModel GetPostViewModel(
            this ICollection<PostEntity> posts, 
            int? pageNumber, 
            ISettingService settingService, 
            string rootUrl)
        {
            int pn = 1;

            if (pageNumber != null)
            {
                pn = (int)pageNumber;
            }

            int pageCount = GetPageCount(posts.Count, settingService.BlogPostsPerPage);

            PostViewModel viewModel = GetPostViewModel(pn, pageCount);

            List<PostEntity> postList =
                posts.Skip((pn - 1) * settingService.BlogPostsPerPage).Take(settingService.BlogPostsPerPage).ToList();

            viewModel.PostItems = GetPostItems(postList, rootUrl);

            viewModel.BlogName = settingService.BlogName;
            viewModel.BlogCaption = settingService.BlogCaption;

            viewModel.CurrentPageNumber = pageNumber.HasValue ? pageNumber.Value : 1;

            return viewModel;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get page count.
        /// </summary>
        /// <param name="totalItems">
        /// The total items.
        /// </param>
        /// <param name="postsPerPage">
        /// The posts per page.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private static int GetPageCount(int totalItems, int postsPerPage)
        {
            var totalPages = (int)Math.Ceiling((decimal)totalItems / postsPerPage);
            return totalPages;
        }

        /// <summary>
        /// The get post items.
        /// </summary>
        /// <param name="postList">
        /// The post list.
        /// </param>
        /// <param name="rootUrl">
        /// The root url.
        /// </param>
        /// <returns>
        /// The <see cref="List{PostItem}"/>.
        /// </returns>
        private static List<PostItem> GetPostItems(List<PostEntity> postList, string rootUrl)
        {
            string url = rootUrl.TrimEnd('/');

            var postItems = new List<PostItem>();

            postList.ForEach(
                post =>
                    {
                        var postModel = new PostItem { Post = post, RootUrl = url };
                        postItems.Add(postModel);
                    });

            return postItems;
        }

        /// <summary>
        /// The get post view model.
        /// </summary>
        /// <param name="currentPage">
        /// The current page.
        /// </param>
        /// <param name="totalPages">
        /// The total pages.
        /// </param>
        /// <returns>
        /// The <see cref="PostViewModel"/>.
        /// </returns>
        private static PostViewModel GetPostViewModel(int currentPage, int totalPages)
        {
            return new PostViewModel
                       {
                           NextPageValid = currentPage != 1 && totalPages > 1, 
                           NextPageNumber = currentPage - 1, 
                           PreviousPageValid = currentPage < totalPages && currentPage != totalPages, 
                           PreviousPageNumber = currentPage + 1
                       };
        }

        #endregion
    }
}