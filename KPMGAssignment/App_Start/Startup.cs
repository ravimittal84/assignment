using Autofac;
using Autofac.Integration.Mvc;
using KPMGAssignment.Persistence;
using KPMGAssignment.Services;
using Owin;
using System.Reflection;
using System.Web.Mvc;

[assembly: Microsoft.Owin.OwinStartup(typeof(KPMGAssignment.Startup))]
namespace KPMGAssignment
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            IContainer container = CreateContainer();
            app.UseAutofacMiddleware(container);

            // Register MVC Types 
            app.UseAutofacMvc();
        }


        private IContainer CreateContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            Assembly assembly = Assembly.GetExecutingAssembly();

            RegisterServices(builder);
            RegisterMvc(builder);

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            return container;
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<TransactionService>().As<ITransactionService>().InstancePerRequest();
            builder.RegisterType<AppDbContext>().InstancePerRequest();
            builder.RegisterType<Repository>().As<IRepository>().InstancePerRequest();
        }

        private static void RegisterMvc(ContainerBuilder builder)
        {
            // Register MVC Controllers
            //builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // Register Common MVC Types
            builder.RegisterModule<AutofacWebTypesModule>();

            // Register MVC Filters
            builder.RegisterFilterProvider();
        }
    }
}
