// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserService.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The user service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Implements
{
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

    using iBlog.Domain.Entities;
    using iBlog.Domain.Interfaces;

    /// <summary>
    /// The user service.
    /// </summary>
    public class UserService : DisposableObject, IUserService
    {
        #region Fields

        /// <summary>
        /// The user table.
        /// </summary>
        private readonly Table<UserEntity> userTable;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        public UserService()
        {
            this.userTable = this.Context.GetTable<UserEntity>();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="UserService"/> class. 
        /// </summary>
        ~UserService()
        {
            this.Dispose(false);
        }

        #endregion

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
        public bool AddUser(string emailAddress, string displayName, string activationTicket)
        {
            bool status = false;
            UserEntity item = this.userTable.SingleOrDefault(u => u.EmailAddress == emailAddress);

            if (item == null)
            {
                var user = new UserEntity
                               {
                                   UserName = string.Empty, 
                                   DisplayName = displayName, 
                                   Password = string.Empty, 
                                   UserCode = string.Empty, 
                                   EmailAddress = emailAddress, 
                                   ActiveStatus = null, 
                                   ActivationKey = activationTicket
                               };

                this.userTable.InsertOnSubmit(user);
                this.Context.SubmitChanges();

                status = true;
            }
            else
            {
                if (item.ID != 1)
                {
                    item.DisplayName = displayName;
                    item.ActivationKey = activationTicket;

                    this.Context.SubmitChanges();

                    status = true;
                }
            }

            return status;
        }

        /// <summary>
        /// The delete user.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        public void DeleteUser(int userID)
        {
            UserEntity user = this.userTable.SingleOrDefault(u => u.ID == userID);
            if (user != null)
            {
                this.userTable.DeleteOnSubmit(user);
                this.Context.SubmitChanges();
            }
        }

        /// <summary>
        /// The forgot password.
        /// </summary>
        /// <param name="emailAddress">
        /// The email address.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string ForgotPassword(string emailAddress)
        {
            UserEntity user = this.userTable.SingleOrDefault(u => u.EmailAddress == emailAddress);
            if (user != null)
            {
                string userString = string.Format(
                    "{0}-{1}-{2}-{3}", 
                    user.UserName, 
                    user.Password, 
                    user.EmailAddress, 
                    DateTime.Now);

                string verificationCode = userString.GetMD5Hash();
                user.ActivationKey = verificationCode;

                this.Context.SubmitChanges();

                return verificationCode;
            }

            return string.Empty;
        }

        /// <summary>
        /// The get all users.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{UserEntity}"/>.
        /// </returns>
        public IEnumerable<UserEntity> GetAllUsers()
        {
            return this.userTable.AsEnumerable();
        }

        /// <summary>
        /// The get one time token.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetOneTimeToken(int userId)
        {
            UserEntity user = this.userTable.SingleOrDefault(u => u.ID == userId);
            return user != null ? user.OneTimeToken : null;
        }

        /// <summary>
        /// The get user by email address.
        /// </summary>
        /// <param name="emailAddress">
        /// The email address.
        /// </param>
        /// <returns>
        /// The <see cref="UserEntity"/>.
        /// </returns>
        public UserEntity GetUserByEmailAddress(string emailAddress)
        {
            UserEntity userObj = this.userTable.SingleOrDefault(u => u.EmailAddress == emailAddress);
            return userObj;
        }

        /// <summary>
        /// The get user by user id.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="UserEntity"/>.
        /// </returns>
        public UserEntity GetUserByUserID(int userID)
        {
            UserEntity user = this.userTable.SingleOrDefault(u => u.ID == userID);
            return user;
        }

        /// <summary>
        /// The get user by user name.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <returns>
        /// The <see cref="UserEntity"/>.
        /// </returns>
        public UserEntity GetUserByUserName(string userName)
        {
            UserEntity user = this.userTable.SingleOrDefault(u => u.UserName == userName);
            return user;
        }

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
        public UserEntity GetUserByUserName(string userName, string password)
        {
            UserEntity user = this.userTable.SingleOrDefault(u => u.UserName == userName && u.Password == password);
            return user;
        }

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
        public bool ResetPassword(string emailAddress, string verificationCode, string newPassword, string userCode)
        {
            UserEntity user =
                this.userTable.SingleOrDefault(
                    u => u.EmailAddress == emailAddress && u.ActivationKey == verificationCode);
            if (user != null)
            {
                user.ActivationKey = string.Empty;
                user.Password = newPassword;
                user.UserCode = userCode;
                this.Context.SubmitChanges();

                return true;
            }

            return false;
        }

        /// <summary>
        /// The set one time token.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="oneTimeToken">
        /// The one time token.
        /// </param>
        public void SetOneTimeToken(int userId, string oneTimeToken)
        {
            UserEntity user = this.userTable.SingleOrDefault(u => u.ID == userId);
            if (user != null)
            {
                user.OneTimeToken = oneTimeToken;
                this.Context.SubmitChanges();
            }
        }

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
        public bool ToggleUserActiveStatus(int userId, bool activate)
        {
            int status = activate ? 1 : 0;
            UserEntity user = this.userTable.SingleOrDefault(u => u.ID == userId);
            if (user != null)
            {
                user.ActiveStatus = status;
                this.Context.SubmitChanges();

                return true;
            }

            return false;
        }

        /// <summary>
        /// The update last login time.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        public void UpdateLastLoginTime(int userID)
        {
            UserEntity user = this.userTable.SingleOrDefault(u => u.ID == userID);
            if (user != null)
            {
                user.LastLoginTime = DateTime.Now;
                this.Context.SubmitChanges();
            }
        }

        /// <summary>
        /// The update profile.
        /// </summary>
        /// <param name="userEntity">
        /// The user entity.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool UpdateProfile(UserEntity userEntity)
        {
            UserEntity user = this.userTable.SingleOrDefault(u => u.ID == userEntity.ID);
            if (user != null)
            {
                if (string.IsNullOrEmpty(user.UserName))
                {
                    user.UserName = userEntity.UserName;
                }

                user.EmailAddress = userEntity.EmailAddress;
                user.DisplayName = userEntity.DisplayName;

                if (userEntity.Password != null && userEntity.UserCode != null)
                {
                    user.Password = userEntity.Password;
                    user.UserCode = userEntity.UserCode;
                }

                if (userEntity.ActiveStatus.HasValue)
                {
                    user.ActiveStatus = userEntity.ActiveStatus;
                }

                user.ActivationKey = string.Empty;
                user.UserSite = userEntity.UserSite;

                this.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        /// <summary>
        /// The update user.
        /// </summary>
        /// <param name="userEntity">
        /// The user entity.
        /// </param>
        public void UpdateUser(UserEntity userEntity)
        {
            UserEntity user = this.userTable.SingleOrDefault(u => u.ID == userEntity.ID);
            if (user != null)
            {
                user.Password = userEntity.Password;
                user.UserCode = userEntity.UserCode;

                this.Context.SubmitChanges();
            }
        }

        #endregion
    }
}