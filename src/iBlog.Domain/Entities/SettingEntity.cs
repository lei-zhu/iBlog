// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingEntity.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The setting entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Entities
{
    using System.Data.Linq.Mapping;

    /// <summary>
    /// The setting entity.
    /// </summary>
    [Table(Name = "Settings")]
    public class SettingEntity
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the key name.
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = false)]
        public string KeyName { get; set; }

        /// <summary>
        /// Gets or sets the key value.
        /// </summary>
        [Column]
        public string KeyValue { get; set; }

        #endregion
    }
}