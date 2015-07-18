// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DependencyModule.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The dependency module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Application
{
    using Autofac;

    using iBlog.Domain.Implements;
    using iBlog.Domain.Interfaces;
    using iBlog.Service;

    /// <summary>
    /// The dependency module.
    /// </summary>
    public class DependencyModule : Module
    {
        #region Methods

        /// <summary>
        /// The load.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CacheService>().As<ICacheService>().SingleInstance();

            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
            builder.RegisterType<RoleService>().As<IRoleService>().SingleInstance();

            builder.RegisterType<CategoryService>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<TagService>().As<ITagService>().SingleInstance();
            builder.RegisterType<CommentService>().As<ICommentService>().SingleInstance();
            builder.RegisterType<PostService>().As<IPostService>().SingleInstance();

            builder.RegisterType<SettingService>().As<ISettingService>().SingleInstance();
            builder.RegisterType<ErrorService>().As<IErrorService>().SingleInstance();
        }

        #endregion
    }
}