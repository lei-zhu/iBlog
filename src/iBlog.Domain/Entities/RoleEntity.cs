// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleEntity.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The role entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Entities
{
    using System.Data.Linq.Mapping;

    /// <summary>
    /// The role entity.
    /// </summary>
    [Table(Name = "Roles")]
    public class RoleEntity
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [Column]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public short ID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Column]
        public string Name { get; set; }

        #endregion
    }
}