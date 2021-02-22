using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.AutofacExtensions.Configuration;
using Core.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.AutofacExtensions
{
    public class ContainerManager : IContainerManager
    {
        void IContainerManager.BuildContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterModule(new ConfigurationSettingsReader("autofac"));

            ContainerContext.Current.Container = builder.Build();
        }

        void IContainerManager.BuildContainer(IApplicationBuilder appBuilder)
        {
        }

        void IContainerManager.BuildContainer(IServiceCollection services)
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterModule(new ConfigurationSettingsReader("autofac"));
            builder.Populate(services);

            ContainerContext.Current.Container = builder.Build();
            ContainerContext.Current.ServiceProvider = new AutofacServiceProvider(ContainerContext.Current.Container);
        }
        
        IComponentLocator IContainerManager.GetLocator()
        {
            return ContainerContext.Current.Container.Resolve<IComponentLocator>();
        }

        IServiceProvider IContainerManager.GetServiceProvider()
        {            
            return ContainerContext.Current.ServiceProvider;
        }
    }
}
