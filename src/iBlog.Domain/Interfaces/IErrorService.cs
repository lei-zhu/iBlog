// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IErrorService.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The ErrorService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Interfaces
{
    using System;

    /// <summary>
    /// The ErrorService interface.
    /// </summary>
    public interface IErrorService
    {
        #region Public Methods and Operators

        /// <summary>
        /// The add error.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        void AddError(Exception exception);

        #endregion
    }
}