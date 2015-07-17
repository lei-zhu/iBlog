// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DisposableObject.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The disposable object.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain
{
    using System;
    using System.Data.Linq;

    /// <summary>
    /// The disposable object.
    /// </summary>
    public class DisposableObject
    {
        #region Fields

        /// <summary>
        /// The context.
        /// </summary>
        protected readonly DataContext Context;

        /// <summary>
        /// The dispose lock.
        /// </summary>
        private readonly object disposeLock = new object();

        /// <summary>
        /// The disposed.
        /// </summary>
        private bool disposed;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableObject"/> class.
        /// </summary>
        protected DisposableObject()
        {
            this.Context = new DataContext(DomainConfiguration.ConnectionString);
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            lock (this.disposeLock)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        this.Context.Dispose();
                    }

                    this.disposed = true;
                }
            }
        }

        #endregion
    }
}