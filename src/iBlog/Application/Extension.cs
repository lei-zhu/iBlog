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
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    using iBlog.Configuration;
    using iBlog.Domain.Entities;
    using iBlog.Domain.Interfaces;
    using iBlog.Exceptions;
    using iBlog.Models;

    /// <summary>
    /// The extension.
    /// </summary>
    public static class Extension
    {
        #region Constants

        /// <summary>
        /// The theme base path.
        /// </summary>
        private const string ThemeBasePath = "~/Bootswatch/{0}";

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The create menu item.
        /// </summary>
        /// <param name="helper">
        /// The helper.
        /// </param>
        /// <param name="menuItem">
        /// The menu item.
        /// </param>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString CreateMenuItem(this HtmlHelper helper, MenuItem menuItem)
        {
            var builder = new StringBuilder();

            var li = new TagBuilder("li");
            if (menuItem.Selected)
            {
                li.AddCssClass("active");
            }

            var tag = new TagBuilder("a");

            string value = menuItem.Url != "/" ? menuItem.Url.ToLower() : "/home/index";

            tag.MergeAttribute("href", value);
            tag.InnerHtml = menuItem.Title;

            li.InnerHtml = tag.ToString();
            builder.AppendLine(li.ToString());

            return MvcHtmlString.Create(builder.ToString());
        }

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
        /// The load theme styles.
        /// </summary>
        /// <param name="helper">
        /// The helper.
        /// </param>
        /// <param name="themeName">
        /// The theme name.
        /// </param>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString LoadThemeStyles(this HtmlHelper helper, string themeName)
        {
            UrlHelper urlHelper = helper.GetUrlHelper();
            var stringBuilder = new StringBuilder();

            string basePath = MapPath(string.Format(ThemeBasePath, themeName));
            List<string> cssFiles = Directory.GetFiles(basePath, "*.min.css").ToList();

            cssFiles.ForEach(
                file =>
                    {
                        var themeStyle = new TagBuilder("link");

                        string value =
                            urlHelper.Content(
                                string.Format(
                                    "{0}/{1}",
                                    string.Format(ThemeBasePath, themeName),
                                    Path.GetFileName(file)));

                        themeStyle.MergeAttribute("href", value);
                        themeStyle.MergeAttribute("rel", "stylesheet");
                        themeStyle.MergeAttribute("type", "text/css");

                        stringBuilder.AppendLine(themeStyle.ToString());
                    });

            return MvcHtmlString.Create(stringBuilder.ToString());
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

        /// <summary>
        /// The map path.
        /// </summary>
        /// <param name="relativePath">
        /// The relative path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string MapPath(string relativePath)
        {
            return HttpContext.Current.Server.MapPath(relativePath);
        }

        #endregion
    }
}