using Web365Business.Front_End.IRepository;
using Web365Business.Front_End.Repository;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365DA.RDBMS.Front_End.Repository;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Web365.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Web365.App_Start.NinjectWebCommon), "Stop")]

namespace Web365.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            #region DA
            //kernel.Bind<IAccountDARepositoryFE>().To<AccountDARepositoryFE>();
            kernel.Bind<IMenuDAFERepository>().To<MenuDAFERepository>();
            kernel.Bind<ILayoutContentDAFERepository>().To<LayoutContentDAFERepository>();
            kernel.Bind<IArticleDAFERepository>().To<ArticleDAFERepository>();
            kernel.Bind<IArticleTypeDAFERepository>().To<ArticleTypeDAFERepository>();
            kernel.Bind<ILanguageDAFERepository>().To<LanguageDAFERepository>();
            kernel.Bind<IOtherDAFERepository>().To<OtherDAFERepository>();
            kernel.Bind<ISettingDAFERepository>().To<SettingDAFERepository>();
            kernel.Bind<IFileDAFERepository>().To<FileDAFERepository>();
            kernel.Bind<IVideoDAFERepository>().To<VideoDAFERepository>();
            kernel.Bind<IPictureDAFERepository>().To<PictureDAFERepository>();
            #endregion

            #region Business

            //Font-end
            //kernel.Bind<IAccountRepositoryFE>().To<AccountRepositoryFE>();
            kernel.Bind<IMenuRepositoryFE>().To<MenuRepositoryFE>();
            kernel.Bind<ILayoutContentRepositoryFE>().To<LayoutContentRepositoryFE>();
            kernel.Bind<IArticleRepositoryFE>().To<ArticleRepositoryFE>();
            kernel.Bind<IArticleTypeRepositoryFE>().To<ArticleTypeRepositoryFE>();
            kernel.Bind<ILanguageRepositoryFE>().To<LanguageRepositoryFE>();
            kernel.Bind<IOtherRepositoryFE>().To<OtherRepositoryFE>();
            kernel.Bind<ISettingRepositoryFE>().To<SettingRepositoryFE>();
            kernel.Bind<IFileRepositoryFE>().To<FileRepositoryFE>();
            kernel.Bind<IVideoRepositoryFE>().To<VideoRepositoryFE>();
            kernel.Bind<IPictureRepositoryFE>().To<PictureRepositoryFE>();
            #endregion
        }
    }
}
