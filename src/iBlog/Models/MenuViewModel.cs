// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuViewModel.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The menu view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The menu view model.
    /// </summary>
    public class MenuViewModel
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the menu items.
        /// </summary>
        public List<MenuItem> MenuItems { get; set; }

        #endregion
    }

    /// <summary>
    /// The menu item.
    /// </summary>
    public class MenuItem
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether selected.
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string Url { get; set; }

        #endregion
    }
}