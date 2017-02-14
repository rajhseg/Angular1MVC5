using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using PurchaseModel;
using PurchaseModel.Repositories;
using PurchaseModel.Repositories.AccountRepositories;
using PurchaseModel.Services;
using PurchaseService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Angular1MVC.App_Start
{
    public class AutofacConfig
    {
        public static IContainer Container;
               
        public static void Initialize(HttpConfiguration configuration)
        {
            Initialize(configuration, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration configuration, IContainer container)
        {
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(RegisterMvcService(new ContainerBuilder())));
        }

        private static IContainer RegisterMvcService(ContainerBuilder builder)
        {

            RegisterCommonTypes(builder);
            builder.RegisterControllers(typeof(MvcApplication).Assembly).InstancePerRequest();
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);
            builder.RegisterModule<AutofacWebTypesModule>();
            Container = builder.Build();
            return Container;

        }
        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            RegisterCommonTypes(builder);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            Container = builder.Build();
            return Container;

        }

        private static void RegisterCommonTypes(ContainerBuilder builder)
        {
            builder.RegisterType<PurchaseDBContext>().As<DbContext>().InstancePerRequest();
            builder.RegisterType<ContextFactory>().As<IContextFactory>().InstancePerRequest();
            builder.RegisterType<EncryptService>().As<IEncryptService>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerRequest();

            builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerRequest();
            builder.RegisterType<UserRoleRepository>().As<IUserRoleRepository>().InstancePerRequest();
        }
    }
}