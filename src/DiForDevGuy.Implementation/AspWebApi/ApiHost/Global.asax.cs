using Autofac;
using Autofac.Integration.WebApi;
using Lib;
using Lib.Abstractions;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace ApiHost
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<AvengerRepository>().As<IAvengerRepository>().InstancePerRequest();
            builder.RegisterType<Logger>().As<ILogger>().InstancePerRequest();

            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            // or
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Controller")).InstancePerRequest();

            IContainer container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
