using Core.Common;
using System;
using Unity;

namespace Core.UnityExtensions
{
    public class ComponentLocator : IComponentLocator
    {
        T IComponentLocator.ResolveComponent<T>()
        {
            return ContainerContext.Current.Container.Resolve<T>();
        }

        T IComponentLocator.ResolveComponent<T>(string key)
        {
            return ContainerContext.Current.Container.Resolve<T>();
        }
    }
}
