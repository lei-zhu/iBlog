// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UrlNotFoundException.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The url not found exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Exceptions
{
    using System;

    /// <summary>
    /// The url not found exception.
    /// </summary>
    public class UrlNotFoundException : Exception
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlNotFoundException"/> class.
        /// </summary>
        public UrlNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlNotFoundException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        public UrlNotFoundException(string message, params object[] parameters)
            : base(string.Format(message, parameters))
        {
        }

        #endregion
    }
}