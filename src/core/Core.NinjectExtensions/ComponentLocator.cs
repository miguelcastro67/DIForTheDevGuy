using Core.Common;
using Ninject;
using System;

namespace Core.NinjectExtensions
{
    public class ComponentLocator : IComponentLocator
    {
        T IComponentLocator.ResolveComponent<T>()
        {
            return ContainerContext.Current.Container.Get<T>();
        }

        T IComponentLocator.ResolveComponent<T>(string key)
        {
            return ContainerContext.Current.Container.Get<T>(key);
        }
    }
}
