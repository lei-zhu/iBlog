// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The app.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Application
{
    using Autofac;
    using Autofac.Builder;

    /// <summary>
    /// The app.
    /// </summary>
    public class App
    {
        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="App"/> class from being created.
        /// </summary>
        private App()
        {
            this.ContainerBuilder = new ContainerBuilder();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static App Instance
        {
            get
            {
                return Nested.Inner;
            }
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        public IContainer Container { get; private set; }

        /// <summary>
        /// Gets the container builder.
        /// </summary>
        public ContainerBuilder ContainerBuilder { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The build.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        /// <returns>
        /// The <see cref="IContainer"/>.
        /// </returns>
        public IContainer Build(ContainerBuildOptions options = ContainerBuildOptions.None)
        {
            return this.Container ?? (this.Container = this.ContainerBuilder.Build(options));
        }

        /// <summary>
        /// The register module.
        /// </summary>
        /// <typeparam name="T">
        /// The generic type.
        /// </typeparam>
        public void RegisterModule<T>() where T : Module, new()
        {
            this.ContainerBuilder.RegisterModule<T>();
        }

        #endregion

        /// <summary>
        /// The nested.
        /// </summary>
        private static class Nested
        {
            #region Static Fields

            /// <summary>
            /// The inner.
            /// </summary>
            internal static readonly App Inner = new App();

            #endregion
        }
    }
}