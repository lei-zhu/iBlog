// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlHelperExtension.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The html helper extension.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Application
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;

    using iBlog.Models;

    /// <summary>
    /// The html helper extension.
    /// </summary>
    public static class HtmlHelperExtension
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
        /// The get required string.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="requiredString">
        /// The required string.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetRequiredString(this HtmlHelper htmlHelper, string requiredString)
        {
            return htmlHelper.ViewContext.RouteData.GetRequiredString(requiredString);
        }

        /// <summary>
        /// The get url helper.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <returns>
        /// The <see cref="UrlHelper"/>.
        /// </returns>
        public static UrlHelper GetUrlHelper(this HtmlHelper htmlHelper)
        {
            return new UrlHelper(htmlHelper.ViewContext.RequestContext);
        }

        /// <summary>
        /// The is authenticated.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsAuthenticated(this HtmlHelper htmlHelper)
        {
            return htmlHelper.ViewContext.RequestContext.HttpContext.Request.IsAuthenticated;
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

            string basePath = Extension.MapPath(string.Format(ThemeBasePath, themeName));
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
    }
}