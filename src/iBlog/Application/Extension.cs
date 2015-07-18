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
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    using iBlog.Configuration;
    using iBlog.Domain.Interfaces;

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
                        themeStyle.MergeAttribute(
                            "href",
                            urlHelper.Content(
                                string.Format(
                                    "{0}/{1}",
                                    string.Format(ThemeBasePath, themeName),
                                    Path.GetFileName(file))));
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