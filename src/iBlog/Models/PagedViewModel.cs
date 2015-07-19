// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PagedViewModel.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The paged view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Models
{
    /// <summary>
    /// The paged view model.
    /// </summary>
    public class PagedViewModel
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        public int CurrentPageNumber { get; set; }

        /// <summary>
        /// Gets or sets the next page number.
        /// </summary>
        public int NextPageNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether next page valid.
        /// </summary>
        public bool NextPageValid { get; set; }

        /// <summary>
        /// Gets or sets the previous page number.
        /// </summary>
        public int PreviousPageNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether previous page valid.
        /// </summary>
        public bool PreviousPageValid { get; set; }

        #endregion
    }
}