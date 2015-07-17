// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMembershipService.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The MembershipService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Service
{
    using System.Web.Security;

    /// <summary>
    /// The MembershipService interface.
    /// </summary>
    public interface IMembershipService
    {
        #region Public Properties

        /// <summary>
        /// Gets the min password length.
        /// </summary>
        int MinPasswordLength { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The change password.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="oldPassword">
        /// The old password.
        /// </param>
        /// <param name="newPassword">
        /// The new password.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool ChangePassword(string userName, string oldPassword, string newPassword);

        /// <summary>
        /// The create user.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <param name="emailAddress">
        /// The email address.
        /// </param>
        /// <returns>
        /// The <see cref="MembershipCreateStatus"/>.
        /// </returns>
        MembershipCreateStatus CreateUser(string userName, string password, string emailAddress);

        /// <summary>
        /// The validate user.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool ValidateUser(string userName, string password);

        #endregion
    }
}