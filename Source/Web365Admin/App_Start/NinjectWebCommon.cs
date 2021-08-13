[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Web365Admin.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Web365Admin.App_Start.NinjectWebCommon), "Stop")]

namespace Web365Admin.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Web365DA.RDBMS.Back_End.IRepository;
    using Web365DA.RDBMS.Back_End.Repository;
    using Web365Business.Back_End.IRepository;
    using Web365Business.Back_End.Repository;
    using Microsoft.AspNet.Identity;
    using Web365Admin.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;

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
            //Back-end
            kernel.Bind<IProductDABERepository>().To<ProductDABERepository>();
            kernel.Bind<IProductTypeDABERepository>().To<ProductTypeDABERepository>();
            kernel.Bind<IDistributorDABERepository>().To<DistributorDABERepository>();
            kernel.Bind<IManufacturerDABERepository>().To<ManufacturerDABERepository>();
            kernel.Bind<IProductStatusDABERepository>().To<ProductStatusDABERepository>();
            kernel.Bind<IProductStatusMapDABERepository>().To<ProductStatusMapDABERepository>();
            kernel.Bind<IArticleDABERepository>().To<ArticleDABERepository>();
            kernel.Bind<IArticleGroupDABERepository>().To<ArticleGroupDABERepository>();
            kernel.Bind<IArticleTypeDABERepository>().To<ArticleTypeDABERepository>();
            kernel.Bind<IArticleGroupMapDABERepository>().To<ArticleGroupMapDABERepository>();
            kernel.Bind<IPictureTypeDABERepository>().To<PictureTypeDABERepository>();
            kernel.Bind<IPictureDABERepository>().To<PictureDABERepository>();
            kernel.Bind<IUserPageDABERepository>().To<UserPageDABERepository>();
            kernel.Bind<IUserRoleDABERepository>().To<UserRoleDABERepository>();
            kernel.Bind<IUserDABERepository>().To<UserDABERepository>();
            kernel.Bind<IAdvertiesDABERepository>().To<AdvertiesDABERepository>();
            kernel.Bind<IAdvertiesPictureMapDABERepository>().To<AdvertiesPictureMapDABERepository>();
            kernel.Bind<ISupportTypeDABERepository>().To<SupportTypeDABERepository>();
            kernel.Bind<ISupportDABERepository>().To<SupportDABERepository>();
            kernel.Bind<IFileTypeDABERepository>().To<FileTypeDABERepository>();
            kernel.Bind<IFileDABERepository>().To<FileDABERepository>();
            kernel.Bind<ICustomerDABERepository>().To<CustomerDABERepository>();
            kernel.Bind<IOrderDABERepository>().To<OrderDABERepository>();
            kernel.Bind<IProductFilterDABERepository>().To<ProductFilterDABERepository>();
            kernel.Bind<IProductGroupAttributeDABERepository>().To<ProductGroupAttributeDABERepository>();
            kernel.Bind<IProductAttributeDABERepository>().To<ProductAttributeDABERepository>();
            kernel.Bind<IProductLabelDABERepository>().To<ProductLabelDABERepository>();
            kernel.Bind<ILayoutContentDABERepository>().To<LayoutContentDABERepository>();
            kernel.Bind<IContactDABERepository>().To<ContactDABERepository>();
            kernel.Bind<IReceiveInfoGroupDABERepository>().To<ReceiveInfoGroupDABERepository>();
            kernel.Bind<IReceiveInfoDABERepository>().To<ReceiveInfoDABERepository>();
            kernel.Bind<IArticleGroupTypeDABERepository>().To<ArticleGroupTypeDABERepository>();
            kernel.Bind<IArticleGroupTypeMapDABERepository>().To<ArticleGroupTypeMapDABERepository>();
            kernel.Bind<IProductTypeGroupDABERepository>().To<ProductTypeGroupDABERepository>();
            kernel.Bind<IProductTypeGroupMapDABERepository>().To<ProductTypeGroupMapDABERepository>();
            kernel.Bind<IMenuDABERepository>().To<MenuDABERepository>();
            kernel.Bind<IProductVariantDABERepository>().To<ProductVariantDABERepository>();
            kernel.Bind<IVideoDABERepository>().To<VideoDABERepository>();
            kernel.Bind<IVideoTypeDABERepository>().To<VideoTypeDABERepository>();
            kernel.Bind<IDatabaseDABERepository>().To<DatabaseDABERepository>();
            kernel.Bind<IGroupLayoutContentDABERepository>().To<GroupLayoutContentDABERepository>();
            kernel.Bind<ILanguageDABERepository>().To<LanguageDABERepository>();
            kernel.Bind<ISettingDABERepository>().To<SettingDABERepository>();

            kernel.Bind<IProductRepositoryBE>().To<ProductRepositoryBE>();
            kernel.Bind<IProductTypeRepositoryBE>().To<ProductTypeRepositoryBE>();
            kernel.Bind<IDistributorRepositoryBE>().To<DistributorRepositoryBE>();
            kernel.Bind<IManufacturerRepositoryBE>().To<ManufacturerRepositoryBE>();
            kernel.Bind<IProductStatusRepositoryBE>().To<ProductStatusRepositoryBE>();
            kernel.Bind<IProductStatusMapRepositoryBE>().To<ProductStatusMapRepositoryBE>();
            kernel.Bind<IArticleRepositoryBE>().To<ArticleRepositoryBE>();
            kernel.Bind<IArticleGroupRepositoryBE>().To<ArticleGroupRepositoryBE>();
            kernel.Bind<IArticleTypeRepositoryBE>().To<ArticleTypeRepositoryBE>();
            kernel.Bind<IArticleGroupMapRepositoryBE>().To<ArticleGroupMapRepositoryBE>();
            kernel.Bind<IPictureTypeRepositoryBE>().To<PictureTypeRepositoryBE>();
            kernel.Bind<IPictureRepositoryBE>().To<PictureRepositoryBE>();
            kernel.Bind<IUserPageRepositoryBE>().To<UserPageRepositoryBE>();
            kernel.Bind<IUserRoleRepositoryBE>().To<UserRoleRepositoryBE>();
            kernel.Bind<IUserRepositoryBE>().To<UserRepositoryBE>();
            kernel.Bind<IAdvertiesRepositoryBE>().To<AdvertiesRepositoryBE>();
            kernel.Bind<IAdvertiesPictureMapRepositoryBE>().To<AdvertiesPictureMapRepositoryBE>();
            kernel.Bind<ISupportTypeRepositoryBE>().To<SupportTypeRepositoryBE>();
            kernel.Bind<ISupportRepositoryBE>().To<SupportRepositoryBE>();
            kernel.Bind<IFileTypeRepositoryBE>().To<FileTypeRepositoryBE>();
            kernel.Bind<IFileRepositoryBE>().To<FileRepositoryBE>();
            kernel.Bind<ICustomerRepositoryBE>().To<CustomerRepositoryBE>();
            kernel.Bind<IOrderRepositoryBE>().To<OrderRepositoryBE>();
            kernel.Bind<IProductFilterRepositoryBE>().To<ProductFilterRepositoryBE>();
            kernel.Bind<IProductGroupAttributeRepositoryBE>().To<ProductGroupAttributeRepositoryBE>();
            kernel.Bind<IProductAttributeRepositoryBE>().To<ProductAttributeRepositoryBE>();
            kernel.Bind<IProductLabelRepositoryBE>().To<ProductLabelRepositoryBE>();
            kernel.Bind<ILayoutContentRepositoryBE>().To<LayoutContentRepositoryBE>();
            kernel.Bind<IContactRepositoryBE>().To<ContactRepositoryBE>();
            kernel.Bind<IReceiveInfoGroupRepositoryBE>().To<ReceiveInfoGroupRepositoryBE>();
            kernel.Bind<IReceiveInfoRepositoryBE>().To<ReceiveInfoRepositoryBE>();
            kernel.Bind<IArticleGroupTypeRepositoryBE>().To<ArticleGroupTypeRepositoryBE>();
            kernel.Bind<IArticleGroupTypeMapRepositoryBE>().To<ArticleGroupTypeMapRepositoryBE>();
            kernel.Bind<IProductTypeGroupRepositoryBE>().To<ProductTypeGroupRepositoryBE>();
            kernel.Bind<IProductTypeGroupMapRepositoryBE>().To<ProductTypeGroupMapRepositoryBE>();
            kernel.Bind<IMenuRepositoryBE>().To<MenuRepositoryBE>();
            kernel.Bind<IProductVariantRepositoryBE>().To<ProductVariantRepositoryBE>();
            kernel.Bind<IVideoRepositoryBE>().To<VideoRepositoryBE>();
            kernel.Bind<IVideoTypeRepositoryBE>().To<VideoTypeRepositoryBE>();
            kernel.Bind<IDatabaseRepositoryBE>().To<DatabaseRepositoryBE>();
            kernel.Bind<IGroupLayoutContentRepositoryBE>().To<GroupLayoutContentRepositoryBE>();
            kernel.Bind<ILanguageRepositoryBE>().To<LanguageRepositoryBE>();
            kernel.Bind<ISettingRepositoryBE>().To<SettingRepositoryBE>();

            //for user manager
            kernel.Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>();
            kernel.Bind<UserManager<ApplicationUser>>().ToSelf();
            kernel.Bind<IAuthenticationManager>().ToMethod(c => HttpContext.Current.GetOwinContext().Authentication).InRequestScope();
            kernel.Bind<ApplicationUserManager>().ToMethod(c => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()).InRequestScope();
        }        
    }
}
