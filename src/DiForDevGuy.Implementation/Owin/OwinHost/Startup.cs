using Autofac;
using Autofac.Integration.WebApi;
using Lib;
using Lib.Abstractions;
using Microsoft.Owin;
using Owin;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace OwinHost
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<AvengerRepository>().As<IAvengerRepository>().InstancePerRequest();
            builder.RegisterType<Logger>().As<ILogger>().InstancePerRequest();
            builder.RegisterType<LoggerMiddleware>().InstancePerRequest();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            IContainer container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            appBuilder
                      .UseAutofacWebApi(config) // creates a lifetime scope available through the owin pipeline
                      .UseAutofacMiddleware(container) // this needs to be the first piece of middleware
                      .UseWebApi(config);

            //appBuilder
            //          .UseAutofacWebApi(config)
            //          .UseAutofacLifetimeScopeInjector(container) // this needs to be the first piece of middleware                      
            //          .UseMiddlewareFromContainer<LoggerMiddleware>() // creates a lifetime scope available through the owin pipeline
            //          .UseWebApi(config);
        }
    }
}
