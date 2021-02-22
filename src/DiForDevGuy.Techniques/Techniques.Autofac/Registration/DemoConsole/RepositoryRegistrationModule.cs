using Autofac;
using Lib;
using System;
using System.Linq;

namespace DemoConsole
{
    public class RepositoryRegistrationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(SuperheroService).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .As(t => t.GetInterfaces()?.FirstOrDefault(
                    i => i.Name == "I" + t.Name));
        }
    }
}
