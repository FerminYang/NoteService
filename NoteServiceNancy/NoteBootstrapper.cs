using System.Data.Entity;
using Apworks.Application;
using Apworks.Config.Fluent;
using Apworks.Repositories;
using Apworks.Repositories.EntityFramework;
using Microsoft.Practices.Unity;
using Nancy.Bootstrappers.Unity;

namespace NoteServiceNancy
{
    public class NoteBootstrapper : UnityNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(IUnityContainer existingContainer)
        {
            existingContainer.RegisterInstance(new NoteServiceDbContext(), new PerResolveLifetimeManager())
                        .RegisterType<IRepositoryContext, EntityFrameworkRepositoryContext>(new HierarchicalLifetimeManager(),
                            new InjectionConstructor(new ResolvedParameter<NoteServiceDbContext>()))
                        .RegisterType(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));
        }

        protected override void ApplicationStartup(IUnityContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            Database.SetInitializer(new NoteServiceInitializer());
            AppRuntime.Instance
                .ConfigureApworks()
                .UsingUnityContainerWithDefaultSettings()
                .Create()
                .Start();
        }
    }
}