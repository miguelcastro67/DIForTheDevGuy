using Castle.Windsor;
using Castle.Windsor.MsDependencyInjection;
using Core.CastleWindsorExtensions.Configuration;
using Core.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.CastleWindsorExtensions
{
    public class ContainerManager : IContainerManager
    {
        void IContainerManager.BuildContainer()
        {
            IWindsorContainer container = new WindsorContainer();

            container.Install(new ConfigurationSettingsReader("castleWindsor"));
            
            ContainerContext.Current.Container = container;
        }

        void IContainerManager.BuildContainer(IApplicationBuilder appBuilder)
        {
        }

        void IContainerManager.BuildContainer(IServiceCollection services)
        {
            IWindsorContainer container = new WindsorContainer();

            container.Install(new ConfigurationSettingsReader("castleWindsor"));

            IServiceProvider serviceProvider = WindsorRegistrationHelper.CreateServiceProvider(container, services);

            ContainerContext.Current.Container = container;
            ContainerContext.Current.ServiceProvider = serviceProvider;
        }

        IComponentLocator IContainerManager.GetLocator()
        {
            return ContainerContext.Current.Container.Resolve<IComponentLocator>();
        }
        
        IServiceProvider IContainerManager.GetServiceProvider()
        {
            IServiceProvider serviceProvider = ContainerContext.Current.ServiceProvider;

            return serviceProvider;
        }
    }
}
