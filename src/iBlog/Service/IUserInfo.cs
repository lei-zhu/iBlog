// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserInfo.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The UserInfo interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Service
{
    /// <summary>
    /// The UserInfo interface.
    /// </summary>
    public interface IUserInfo
    {
        #region Public Properties

        /// <summary>
        /// Gets the user id.
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// Gets the user token.
        /// </summary>
        string UserToken { get; }

        #endregion
    }
}