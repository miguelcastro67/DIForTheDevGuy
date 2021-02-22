using Autofac;
using Autofac.Integration.Mvc;
using Lib;
using Lib.Abstractions;
using MvcClient.Controllers;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcClient
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<AvengerRepository>().As<IAvengerRepository>().InstancePerRequest();
            builder.RegisterType<Logger>().As<ILogger>().InstancePerRequest();
            builder.RegisterType<SuperheroService>().As<ISuperheroService>().InstancePerRequest();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            // or
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            //    .Where(t => t.Name.EndsWith("Controller")).InstancePerRequest();

            IContainer container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
