using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Lib;
using System;
using System.Linq;

namespace DemoConsole
{
    public class RepositoryRegistrationModule : IWindsorInstaller
    {
        void IWindsorInstaller.Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssembly(typeof(SuperheroService).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .WithService.Select((t, b) => new[] { t.GetInterfaces()?.FirstOrDefault(
                    i => i.Name == "I" + t.Name) }));
        }
    }
}
