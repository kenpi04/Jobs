using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using Job.Data;
using Job.Services.WorkContext;
using Job.Web.Controllers;
using Job.Services;
using Job.Core.Caching;
using System.Reflection;
using System.Web.Http;

namespace Job.Web.Infrastructure
{
    public static class DependencyRegistrar
    {
        
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            //register dependencies in service
            builder.Register<IDbContext>(c => new DataContext("name=JobConnection")).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            //HTTP context base
            builder.Register(c => new HttpContextWrapper(HttpContext.Current)).As<HttpContextBase>().InstancePerRequest();
            //cache
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("nop_cache_static").SingleInstance();
            builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>().Named<ICacheManager>("nop_cache_per_request").InstancePerLifetimeScope();

            //  builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();
            //  builder.RegisterType<WorkContext>().As<IWorkContext>().InstancePerRequest();
            //Get list service
            var typesToRegisterService = RegisterDependencyService.GetListServiceAssembly();
            // .Where(type => type.BaseType != null && type.BaseType.IsClass&&type.Namespace.StartsWith("PL.Service"));
            foreach (var type in typesToRegisterService)
            {
                var typeInterFace = type.GetInterfaces().FirstOrDefault(x => x.Name == string.Format("I{0}", type.Name));

                builder.RegisterType(type).As(new Type[] { typeInterFace }).InstancePerRequest();
            }
            // Register dependencies in controllers
            //  builder.RegisterControllers(typeof(AccountController).Assembly);
            var typesToRegisterController = Assembly.GetExecutingAssembly().GetTypes()
      .Where(type => !String.IsNullOrEmpty(type.Namespace) && type.Namespace == "Job.Web.Controllers" || type.Namespace == "Job.Web.Areas.Admin.Controllers")
      .Where(type => type.BaseType != null && type.BaseType.IsClass && type.IsVisible);
            foreach (var type in typesToRegisterController)
            {

                builder.RegisterControllers(type.Assembly);


            }
          
            var container = builder.Build();


            // Set the dependency resolver for Web API.         

            // Set the dependency resolver for MVC.
            var mvcResolver = new AutofacDependencyResolver(container);
           DependencyResolver.SetResolver(mvcResolver);
       
          //  EnginerContext.SetBuilder(container);
        }
    }
}