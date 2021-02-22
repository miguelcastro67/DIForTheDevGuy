using Autofac;
using Core.AutofacExtensions.Configuration;
using Core.Common;
using System;

namespace Core.AutofacExtensions
{
    public class ComponentLocator : IComponentLocator
    {
        T IComponentLocator.ResolveComponent<T>()
        {
            return ContainerContext.Current.Container.Resolve<T>();
        }

        T IComponentLocator.ResolveComponent<T>(string key)
        {
            return ContainerContext.Current.Container.ResolveKeyed<T>(key);
        }
    }
}
