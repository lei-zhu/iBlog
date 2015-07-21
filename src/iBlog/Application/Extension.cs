// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extension.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The extension.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Application
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Web;

    using iBlog.Configuration;
    using iBlog.Domain.Entities;
    using iBlog.Domain.Interfaces;
    using iBlog.Exceptions;

    /// <summary>
    /// The extension.
    /// </summary>
    public static class Extension
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get month name.
        /// </summary>
        /// <param name="monthCode">
        /// The month code.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetMonthName(this string monthCode)
        {
            int monthNumber;
            if (!int.TryParse(monthCode, out monthNumber) || monthNumber < 1 || monthNumber > 12)
            {
                throw new InvalidMonthException("An invalid month number was passed", monthCode);
            }

            var formatInfo = new DateTimeFormatInfo();
            return formatInfo.GetMonthName(monthNumber).ToLower();
        }

        /// <summary>
        /// The get pages from cache.
        /// </summary>
        /// <param name="cacheService">
        /// The cache service.
        /// </param>
        /// <param name="postRepository">
        /// The post repository.
        /// </param>
        /// <param name="cacheID">
        /// The cache id.
        /// </param>
        /// <param name="isMarkdown">
        /// The is markdown.
        /// </param>
        /// <returns>
        /// The <see cref="List{PostEntity}"/>.
        /// </returns>
        public static List<PostEntity> GetPagesFromCache(this ICacheService cacheService, IPostService postRepository, string cacheID, bool isMarkdown)
        {
            var markdown = new MarkdownDeep.Markdown { ExtraMode = true };

            var pages = cacheService.Get(cacheID, postRepository.GetAllPages);
            if (isMarkdown)
            {
                pages.ForEach(p => p.Content = markdown.Transform(p.Content));
            }

            return pages;
        }

        /// <summary>
        /// The get posts from cache.
        /// </summary>
        /// <param name="cacheService">
        /// The cache service.
        /// </param>
        /// <param name="postRepository">
        /// The post repository.
        /// </param>
        /// <param name="cacheID">
        /// The cache id.
        /// </param>
        /// <param name="isMarkdown">
        /// The is markdown.
        /// </param>
        /// <returns>
        /// The <see cref="List{PostEntity}"/>.
        /// </returns>
        public static List<PostEntity> GetPostsFromCache(this ICacheService cacheService, IPostService postRepository, string cacheID, bool isMarkdown)
        {
            var markdown = new MarkdownDeep.Markdown { ExtraMode = true };

            var posts = cacheService.Get(cacheID, postRepository.GetAllPosts);
            if (isMarkdown)
            {
                posts.ForEach(p => p.Content = markdown.Transform(p.Content));
            }

            return posts;
        }

        /// <summary>
        /// The find theme.
        /// </summary>
        /// <param name="themeElement">
        /// The theme element.
        /// </param>
        /// <param name="settingService">
        /// The setting service.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string FindTheme(this ThemeElement themeElement, ISettingService settingService)
        {
            string selectedTheme = themeElement.SelectedTheme;

            if (!string.IsNullOrEmpty(selectedTheme) && FolderExists(selectedTheme))
            {
                return selectedTheme;
            }

            selectedTheme = settingService.BlogTheme;

            if (!string.IsNullOrEmpty(selectedTheme) && FolderExists(selectedTheme))
            {
                return selectedTheme;
            }

            return null;
        }

        /// <summary>
        /// The format with.
        /// </summary>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        /// <summary>
        /// The map path.
        /// </summary>
        /// <param name="relativePath">
        /// The relative path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string MapPath(string relativePath)
        {
            return HttpContext.Current.Server.MapPath(relativePath);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The folder exists.
        /// </summary>
        /// <param name="themeName">
        /// The theme name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool FolderExists(string themeName)
        {
            string folderPath = MapPath(string.Format("~/Bootswatch/{0}", themeName));
            return Directory.Exists(folderPath);
        }

        #endregion
    }
}