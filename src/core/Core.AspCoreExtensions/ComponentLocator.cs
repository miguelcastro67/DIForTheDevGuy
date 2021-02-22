using Microsoft.Extensions.DependencyInjection;
using Core.Common;
using System;

namespace Core.AspCoreExtensions
{
    public class ComponentLocator : IComponentLocator
    {
        T IComponentLocator.ResolveComponent<T>()
        {
            return ContainerContext.Current.Container.GetService<T>();
        }
        
        T IComponentLocator.ResolveComponent<T>(string key)
        {
            return default(T);
        }
    }
}
