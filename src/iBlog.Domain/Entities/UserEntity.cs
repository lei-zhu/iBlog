// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserEntity.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The user entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Entities
{
    using System;
    using System.Data.Linq.Mapping;

    /// <summary>
    /// The user entity.
    /// </summary>
    [Table(Name = "Users")]
    public partial class UserEntity
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the activation key.
        /// </summary>
        [Column]
        public string ActivationKey { get; set; }

        /// <summary>
        /// Gets or sets the active status.
        /// </summary>
        [Column]
        public int? ActiveStatus { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        [Column]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [Column]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the last login time.
        /// </summary>
        [Column]
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// Gets or sets the one time token.
        /// </summary>
        [Column]
        public string OneTimeToken { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Column]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the user code.
        /// </summary>
        [Column]
        public string UserCode { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        [Column]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the user site.
        /// </summary>
        [Column]
        public string UserSite { get; set; }

        #endregion
    }
}