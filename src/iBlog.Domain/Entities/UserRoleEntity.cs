// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRoleEntity.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The user role entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Entities
{
    using System.Data.Linq.Mapping;

    /// <summary>
    /// The user role entity.
    /// </summary>
    [Table(Name = "UserRoles")]
    public class UserRoleEntity
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the role id.
        /// </summary>
        [Column]
        public short RoleID { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        [Column]
        public int UserID { get; set; }

        #endregion
    }
}