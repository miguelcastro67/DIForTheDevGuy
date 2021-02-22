using Core.Common;
using Core.UnityExtensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using Unity;
using unityMS = Unity.Microsoft.DependencyInjection;

namespace Core.UnityExtensions
{
    public class ContainerManager : IContainerManager
    {
        void IContainerManager.BuildContainer()
        {
            UnityContainer container = new UnityContainer();

            container.AddNewExtension<ConfigurationSettingsReader>();

            ContainerContext.Current.Container = container;
        }
        
        void IContainerManager.BuildContainer(IApplicationBuilder appBuilder)
        {
        }

        void IContainerManager.BuildContainer(IServiceCollection services)
        {
            IServiceProvider serviceProvider = unityMS.ServiceProvider.ConfigureServices(services);
            IUnityContainer container = serviceProvider.GetService<IUnityContainer>();

            unityMS.ServiceProviderFactory _factory = _factory = new unityMS.ServiceProviderFactory(container);

            services.Replace(ServiceDescriptor.Singleton<IServiceProviderFactory<IUnityContainer>>(_factory));
            services.Replace(ServiceDescriptor.Singleton<IServiceProviderFactory<IServiceCollection>>(_factory));

            container.AddNewExtension<ConfigurationSettingsReader>();

            ContainerContext.Current.Container = container;
            ContainerContext.Current.ServiceProvider = serviceProvider;
        }
        
        IComponentLocator IContainerManager.GetLocator()
        {
            return ContainerContext.Current.ServiceProvider.GetService<IComponentLocator>();
        }

        IServiceProvider IContainerManager.GetServiceProvider()
        {
            return ContainerContext.Current.ServiceProvider;
        }
    }
}
