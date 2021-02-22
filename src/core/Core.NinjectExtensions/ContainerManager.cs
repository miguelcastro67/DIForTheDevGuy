using Core.Common;
using Core.NinjectExtensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using System;
using System.Linq;

namespace Core.NinjectExtensions
{
    public class ContainerManager : IContainerManager
    {
        void IContainerManager.BuildContainer()
        {
        }

        void IContainerManager.BuildContainer(IApplicationBuilder appBuilder)
        {
            IKernel kernel = ContainerContext.Current.Container;
            if (kernel == null)
                kernel = new StandardKernel();

            foreach (var ctrlType in appBuilder.GetControllerTypes())
            {
                kernel.Bind(ctrlType).ToSelf().InScope(x => LifetimeScope.Current);
            }

            foreach (var ctrlType in appBuilder.GetViewTypes())
            {
                kernel.Bind(ctrlType).ToSelf().InScope(x => LifetimeScope.Current);
            }

            kernel.Load<ConfigurationSettingsReader>();

            kernel.BindToMethod(appBuilder.GetRequestService<IViewBufferScope>);

            ContainerContext.Current.Container = kernel;
        }

        void IContainerManager.BuildContainer(IServiceCollection services)
        {
            IKernel kernel = ContainerContext.Current.Container;
            if (kernel == null)
                kernel = new StandardKernel();

            object resolve(Type type) => kernel.Get(type);

            services.AddRequestScopingMiddleware(() => LifetimeScope.Current);
            services.AddCustomControllerActivation(resolve);
            services.AddCustomViewComponentActivation(resolve);

            ContainerContext.Current.Container = kernel;
        }

        IComponentLocator IContainerManager.GetLocator()
        {
            return ContainerContext.Current.Container.Get<IComponentLocator>();
        }

        IServiceProvider IContainerManager.GetServiceProvider()
        {
            return null;
        }
    }
}
