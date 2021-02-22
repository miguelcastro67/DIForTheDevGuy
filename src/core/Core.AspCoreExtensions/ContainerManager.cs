using Core.AspCoreExtensions.Configuration;
using Core.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.AspCoreExtensions
{
    public class ContainerManager : IContainerManager
    {
        void IContainerManager.BuildContainer()
        {
        }

        void IContainerManager.BuildContainer(IApplicationBuilder appBuilder)
        {
        }
        
        void IContainerManager.BuildContainer(IServiceCollection services)
        {           
            services.RegisterModule(new ConfigurationSettingsReader("aspcore"));

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            ContainerContext.Current.Container = services.BuildServiceProvider();
            ContainerContext.Current.ServiceProvider = serviceProvider;
        }
        
        IComponentLocator IContainerManager.GetLocator()
        {
            return ContainerContext.Current.Container.GetService<IComponentLocator>();
        }

        IServiceProvider IContainerManager.GetServiceProvider()
        {
            return ContainerContext.Current.ServiceProvider;
        }
    }
}
