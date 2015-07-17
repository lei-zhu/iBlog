// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorEntity.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The error entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Entities
{
    using System;
    using System.Data.Linq.Mapping;

    /// <summary>
    /// The error entity.
    /// </summary>
    [Table(Name = "Errors")]
    public class ErrorEntity
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
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        [Column]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the throw time.
        /// </summary>
        [Column]
        public DateTime ThrowTime { get; set; }

        #endregion
    }
}