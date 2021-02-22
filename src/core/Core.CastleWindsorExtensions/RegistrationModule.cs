using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Core.Common;
using System;

namespace Core.CastleWindsorExtensions
{
    public class RegistrationModule : IWindsorInstaller
    {
        void IWindsorInstaller.Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ITypeActivator>().ImplementedBy<DiActivator>());
            container.Register(Component.For<IComponentLocator>().ImplementedBy<ComponentLocator>());
        }
    }
}