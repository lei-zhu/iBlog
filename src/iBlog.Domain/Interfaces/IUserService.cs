// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserService.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The UserService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;

    using iBlog.Domain.Entities;

    /// <summary>
    /// The UserService interface.
    /// </summary>
    public interface IUserService : IDisposable
    {
        #region Public Methods and Operators

        /// <summary>
        /// The add user.
        /// </summary>
        /// <param name="emailAddress">
        /// The email address.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        /// <param name="activationTicket">
        /// The activation ticket.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool AddUser(string emailAddress, string displayName, string activationTicket);

        /// <summary>
        /// The delete user.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        void DeleteUser(int userID);

        /// <summary>
        /// The forgot password.
        /// </summary>
        /// <param name="emailAddress">
        /// The email address.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string ForgotPassword(string emailAddress);

        /// <summary>
        /// The get all users.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{UserEntity}"/>.
        /// </returns>
        IEnumerable<UserEntity> GetAllUsers();

        /// <summary>
        /// The get one time token.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetOneTimeToken(int userId);

        /// <summary>
        /// The get user by email address.
        /// </summary>
        /// <param name="emailAddress">
        /// The email address.
        /// </param>
        /// <returns>
        /// The <see cref="UserEntity"/>.
        /// </returns>
        UserEntity GetUserByEmailAddress(string emailAddress);

        /// <summary>
        /// The get user by user id.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="UserEntity"/>.
        /// </returns>
        UserEntity GetUserByUserID(int userID);

        /// <summary>
        /// The get user by user name.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <returns>
        /// The <see cref="UserEntity"/>.
        /// </returns>
        UserEntity GetUserByUserName(string userName);

        /// <summary>
        /// The get user by user name.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// The <see cref="UserEntity"/>.
        /// </returns>
        UserEntity GetUserByUserName(string userName, string password);

        /// <summary>
        /// The reset password.
        /// </summary>
        /// <param name="emailAddress">
        /// The email address.
        /// </param>
        /// <param name="verificationCode">
        /// The verification code.
        /// </param>
        /// <param name="newPassword">
        /// The new password.
        /// </param>
        /// <param name="userCode">
        /// The user code.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool ResetPassword(string emailAddress, string verificationCode, string newPassword, string userCode);

        /// <summary>
        /// The set one time token.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="oneTimeToken">
        /// The one time token.
        /// </param>
        void SetOneTimeToken(int userId, string oneTimeToken);

        /// <summary>
        /// The toggle user active status.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="activate">
        /// The activate.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool ToggleUserActiveStatus(int userId, bool activate);

        /// <summary>
        /// The update last login time.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        void UpdateLastLoginTime(int userID);

        /// <summary>
        /// The update profile.
        /// </summary>
        /// <param name="userEntity">
        /// The user entity.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool UpdateProfile(UserEntity userEntity);

        /// <summary>
        /// The update user.
        /// </summary>
        /// <param name="userEntity">
        /// The user entity.
        /// </param>
        void UpdateUser(UserEntity userEntity);

        #endregion
    }
}