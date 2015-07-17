// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceLocator.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The service locator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Autofac;
    using Autofac.Builder;
    using Autofac.Core;

    using iBlog.Application;

    /// <summary>
    /// The service locator.
    /// </summary>
    public sealed class ServiceLocator
    {
        #region Fields

        /// <summary>
        /// The container.
        /// </summary>
        private readonly IContainer container;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="ServiceLocator"/> class from being created.
        /// </summary>
        private ServiceLocator()
        {
            this.container = App.Instance.Build(ContainerBuildOptions.ExcludeDefaultModules);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static ServiceLocator Instance
        {
            get
            {
                return Nested.Inner;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get service.
        /// </summary>
        /// <param name="serviceType">
        /// The service type.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object GetService(Type serviceType)
        {
            return this.container.Resolve(serviceType);
        }

        /// <summary>
        /// The get service.
        /// </summary>
        /// <typeparam name="T">
        /// The generic type.
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T GetService<T>() where T : class
        {
            return this.container.Resolve<T>();
        }

        /// <summary>
        /// The get service.
        /// </summary>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        /// <typeparam name="T">
        /// The generic type.
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T GetService<T>(object arguments) where T : class
        {
            IEnumerable<Parameter> parameters = GetParameters(arguments);
            return this.container.Resolve<T>(parameters);
        }

        /// <summary>
        /// The get service.
        /// </summary>
        /// <param name="serviceType">
        /// The service type.
        /// </param>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object GetService(Type serviceType, object arguments)
        {
            IEnumerable<Parameter> parameters = GetParameters(arguments);
            return this.container.Resolve(serviceType, parameters);
        }

        /// <summary>
        /// The registered.
        /// </summary>
        /// <typeparam name="T">
        /// The generic type.
        /// </typeparam>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Registered<T>()
        {
            return this.container.IsRegistered<T>();
        }

        /// <summary>
        /// The registered.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Registered(Type type)
        {
            return this.container.IsRegistered(type);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get parameters.
        /// </summary>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{Parameter}"/>.
        /// </returns>
        private static IEnumerable<Parameter> GetParameters(object arguments)
        {
            IList<Parameter> parameters = new List<Parameter>();

            if (null != arguments)
            {
                arguments.GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .ToList()
                    .ForEach(
                        property =>
                            {
                                string propertyName = property.Name;
                                object propertyValue = property.GetValue(arguments, null);

                                parameters.Add(new NamedPropertyParameter(propertyName, propertyValue));
                            });
            }

            return parameters;
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
            internal static readonly ServiceLocator Inner = new ServiceLocator();

            #endregion
        }
    }
}