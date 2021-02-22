using Autofac;
using Autofac.Integration.Web;
using Lib;
using Lib.Abstractions;
using System;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebFormClient
{
    public class Global : HttpApplication, IContainerProviderAccessor
    {
        static IContainerProvider _ContainerProvider;

        public IContainerProvider ContainerProvider
        {
            get { return _ContainerProvider; }
        }

        void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<AvengerRepository>().As<IAvengerRepository>().InstancePerRequest();
            builder.RegisterType<Logger>().As<ILogger>().InstancePerRequest();
            builder.RegisterType<SuperheroService>().As<ISuperheroService>().InstancePerRequest();

            IContainer container = builder.Build();

            // deprecated - see AutofacPageHandlerFactory class
            //Application["Container"] = container;

            _ContainerProvider = new ContainerProvider(container);
        }
    }
}