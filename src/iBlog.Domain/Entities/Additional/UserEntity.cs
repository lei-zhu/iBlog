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
    /// <summary>
    /// The user entity.
    /// </summary>
    public partial class UserEntity
    {
        #region Public Properties

        /// <summary>
        /// Gets the active status string.
        /// </summary>
        public string ActiveStatusString
        {
            get
            {
                if (!this.ActiveStatus.HasValue)
                {
                    return "尚未注册！";
                }

                switch (this.ActiveStatus.Value)
                {
                    case 0:
                        return "Inactive";
                    case 1:
                        return "Active";
                    default:
                        return "Unknown";
                }
            }
        }

        /// <summary>
        /// Gets or sets the posts count.
        /// </summary>
        public int PostsCount { get; set; }

        /// <summary>
        /// Gets or sets the role id.
        /// </summary>
        public short RoleID { get; set; }

        #endregion
    }
}