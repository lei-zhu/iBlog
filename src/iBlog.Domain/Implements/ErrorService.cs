// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorService.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The error service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain.Implements
{
    using System;
    using System.Data.Linq;

    using iBlog.Domain.Entities;
    using iBlog.Domain.Interfaces;

    /// <summary>
    /// The error service.
    /// </summary>
    public class ErrorService : IErrorService
    {
        #region Fields

        /// <summary>
        /// The context.
        /// </summary>
        private readonly DataContext context;

        /// <summary>
        /// The error table.
        /// </summary>
        private readonly Table<ErrorEntity> errorTable;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorService"/> class.
        /// </summary>
        public ErrorService()
        {
            this.context = new DataContext(DomainConfiguration.ConnectionString);
            this.errorTable = this.context.GetTable<ErrorEntity>();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add error.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        public void AddError(Exception exception)
        {
            try
            {
                ErrorEntity errorEntity = exception.ToErrorEntity();
                this.errorTable.InsertOnSubmit(errorEntity);
                this.context.SubmitChanges();
            }
            catch (Exception)
            {
            }
        }

        #endregion
    }
}