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
    using System.Web.Mvc;

    /// <summary>
    /// The html helper extension.
    /// </summary>
    public static class HtmlHelperExtension
    {
        #region Public Methods and Operators

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

        #endregion
    }
}