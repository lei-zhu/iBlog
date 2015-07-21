// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The route config.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.App_Start
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// The route config.
    /// </summary>
    public class RouteConfig
    {
        #region Public Methods and Operators

        /// <summary>
        /// The register routes.
        /// </summary>
        /// <param name="routes">
        /// The routes.
        /// </param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Page",
                "home/index/{page}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new { page = @"\d+" });

            routes.MapRoute(
                "PageByYearMonth",
                "{year}/{month}",
                new { controller = "Home", action = "IndexByYearMonth" },
                new { year = @"\d{4}", month = @"[0-9]{1,2}" });

            routes.MapRoute(
                "PageByCategory",
                "category/{categoryName}",
                new { controller = "Home", action = "IndexByCategory" });

            routes.MapRoute(
                "Post",
                "{year}/{month}/{url}/{status}",
                new { controller = "Home", action = "PostPage", status = UrlParameter.Optional },
                new { year = @"\d{4}", month = @"[0-9]{1,2}", url = @"\S+", status = @"[a-z\-]*" });

            routes.MapRoute(
                "Default", 
                "{controller}/{action}/{id}", 
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

        #endregion
    }
}