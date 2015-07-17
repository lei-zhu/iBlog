// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleService.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The role service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Implements
{
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

    using iBlog.Domain.Entities;
    using iBlog.Domain.Interfaces;

    /// <summary>
    /// The role service.
    /// </summary>
    public class RoleService : DisposableObject, IRoleService
    {
        #region Fields

        /// <summary>
        /// The role table.
        /// </summary>
        private readonly Table<RoleEntity> roleTable;

        /// <summary>
        /// The user role mapping table.
        /// </summary>
        private readonly Table<UserRoleEntity> userRoleMappingTable;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleService"/> class.
        /// </summary>
        public RoleService()
        {
            this.roleTable = this.Context.GetTable<RoleEntity>();
            this.userRoleMappingTable = this.Context.GetTable<UserRoleEntity>();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="RoleService"/> class. 
        /// </summary>
        ~RoleService()
        {
            this.Dispose(false);
        }

        #endregion

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
        public void AddRoleForUser(int userId, short roleId)
        {
            UserRoleEntity oldRole = this.userRoleMappingTable.SingleOrDefault(u => u.ID == userId);
            if (oldRole != null)
            {
                this.userRoleMappingTable.DeleteOnSubmit(oldRole);
                this.Context.SubmitChanges();
            }

            var userRoleEntity = new UserRoleEntity { UserID = userId, RoleID = roleId };
            this.userRoleMappingTable.InsertOnSubmit(userRoleEntity);
            this.Context.SubmitChanges();
        }

        /// <summary>
        /// The get all roles.
        /// </summary>
        /// <returns>
        /// The <see cref="List{RoleEntity}"/>.
        /// </returns>
        public List<RoleEntity> GetAllRoles()
        {
            return this.roleTable.ToList();
        }

        /// <summary>
        /// The get role id for user.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="short"/>.
        /// </returns>
        public short GetRoleIDForUser(int userId)
        {
            UserRoleEntity userRoleEntity = this.userRoleMappingTable.SingleOrDefault(r => r.UserID == userId);
            return (short)(userRoleEntity == null ? -1 : userRoleEntity.RoleID);
        }

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
        public bool IsInRole(int userId, string roleName)
        {
            if (!this.userRoleMappingTable.Any(u => u.ID == userId))
            {
                return false;
            }

            RoleEntity roleEntity = this.roleTable.SingleOrDefault(r => r.Name == roleName);
            return roleEntity != null && this.userRoleMappingTable.Any(u => u.ID == userId && u.RoleID == roleEntity.ID);
        }

        /// <summary>
        /// The remove roles for user.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        public void RemoveRolesForUser(int userId)
        {
            IQueryable<UserRoleEntity> userRoles = this.userRoleMappingTable.Where(u => u.UserID == userId);
            if (userRoles.Any())
            {
                this.userRoleMappingTable.DeleteAllOnSubmit(userRoles);
                this.Context.SubmitChanges();
            }
        }

        #endregion
    }
}