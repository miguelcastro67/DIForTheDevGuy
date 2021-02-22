using Autofac;
using Autofac.Features.ResolveAnything;
using Lib;
using Lib.Abstractions;
using System;
using System.Linq;

namespace AutofacExtensions
{
    public class RegistrationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigurationFactory>().As<IConfigurationFactory>().SingleInstance();
            builder.RegisterType<AvengerRepository>().As<IAvengerRepository>();
            builder.RegisterType<SuperheroService>();

            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource(t =>
            {
                return t.GetInterfaces().FirstOrDefault(i =>
                    i.Name == "ILogger"
                ) != null;
            })); 

            builder.RegisterAssemblyTypes(typeof(SuperheroService).Assembly)
                .Where(t => t.Name.EndsWith("Handler"))
                .As<IAvengerHandler>()
                .Keyed<IAvengerHandler>(t =>
                {
                    string key = t.Name.Replace("Handler", "").ToLower();
                    return key;
                });
        }
    }
}
