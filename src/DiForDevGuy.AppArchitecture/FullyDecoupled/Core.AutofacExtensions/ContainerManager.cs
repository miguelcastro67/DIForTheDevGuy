using Autofac;
using Core.AutofacExtensions.Configuration;
using Core.Common;
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

        IComponentLocator IContainerManager.GetLocator()
        {
            // not using ILifetimeScope here cause this class is not obtained 
            // by the container so cannot inject dependencies
            return ContainerContext.Current.Container.Resolve<IComponentLocator>();
        }
    }
}
