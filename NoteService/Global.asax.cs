using Apworks.Application;
using Apworks.Config.Fluent;
using Apworks.Repositories;
using Apworks.Repositories.EntityFramework;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity.WebApi;

namespace NoteService
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Initialize database
            Database.SetInitializer<NoteServiceDbContext>(new NoteServiceInitializer());

            AppRuntime.Instance
                .ConfigureApworks()
                .UsingUnityContainerWithDefaultSettings()
                .Create((sender, e) =>
                    {
                        var container = e.ObjectContainer.GetWrappedContainer<UnityContainer>();
                        // TODO: register types
                        container.RegisterInstance<NoteServiceDbContext>(new NoteServiceDbContext(), new PerResolveLifetimeManager())
                            .RegisterType<IRepositoryContext, EntityFrameworkRepositoryContext>(new HierarchicalLifetimeManager(),
                                new InjectionConstructor(new ResolvedParameter<NoteServiceDbContext>()))
                            .RegisterType(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));
                        GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
                    }).Start();
        }
    }
}