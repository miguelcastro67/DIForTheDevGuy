using Core.Common;
using Core.NinjectExtensions.Configuration;
using Ninject;
using System;

namespace Core.NinjectExtensions
{
    public class ContainerManager : IContainerManager
    {
        void IContainerManager.BuildContainer()
        {
            IKernel kernel = new StandardKernel();

            kernel.Load<ConfigurationSettingsReader>();

            ContainerContext.Current.Container = kernel;
        }

        IComponentLocator IContainerManager.GetLocator()
        {
            return ContainerContext.Current.Container.Get<IComponentLocator>();
        }
    }
}
