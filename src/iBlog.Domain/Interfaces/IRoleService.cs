// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRoleService.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The RoleService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;

    using iBlog.Domain.Entities;

    /// <summary>
    /// The RoleService interface.
    /// </summary>
    public interface IRoleService : IDisposable
    {
        #region Public Methods and Operators

        /// <summary>
        /// The add role for user.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="roleId">
        /// The role id.
        /// </param>
        void AddRoleForUser(int userId, short roleId);

        /// <summary>
        /// The get all roles.
        /// </summary>
        /// <returns>
        /// The <see cref="List{RoleEntity}"/>.
        /// </returns>
        List<RoleEntity> GetAllRoles();

        /// <summary>
        /// The get role id for user.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="short"/>.
        /// </returns>
        short GetRoleIDForUser(int userId);

        /// <summary>
        /// The is in role.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="roleName">
        /// The role name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool IsInRole(int userId, string roleName);

        /// <summary>
        /// The remove roles for user.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        void RemoveRolesForUser(int userId);

        #endregion
    }
}