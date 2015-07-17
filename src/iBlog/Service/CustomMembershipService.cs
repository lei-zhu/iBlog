// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomMembershipService.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The custom membership service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Service
{
    using System.Web.Security;

    /// <summary>
    /// The custom membership service.
    /// </summary>
    public class CustomMembershipService : IMembershipService
    {
        #region Fields

        /// <summary>
        /// The provider.
        /// </summary>
        private readonly MembershipProvider provider;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomMembershipService"/> class.
        /// </summary>
        public CustomMembershipService()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomMembershipService"/> class.
        /// </summary>
        /// <param name="provider">
        /// The provider.
        /// </param>
        public CustomMembershipService(MembershipProvider provider)
        {
            this.provider = provider ?? Membership.Provider;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the min password length.
        /// </summary>
        public int MinPasswordLength
        {
            get
            {
                return this.provider.MinRequiredPasswordLength;
            }
        }

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
        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            return false;
        }

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
        public MembershipCreateStatus CreateUser(string userName, string password, string emailAddress)
        {
            return MembershipCreateStatus.Success;
        }

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
        public bool ValidateUser(string userName, string password)
        {
            return false;
        }

        #endregion
    }
}