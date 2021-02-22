using Core.Common;
using Ninject;
using System;

namespace Core.NinjectExtensions
{
    public class ComponentLocator : IComponentLocator
    {
        public ComponentLocator(IKernel container)
        {
            _Container = container;
        }

        IKernel _Container;

        T IComponentLocator.ResolveComponent<T>()
        {
            return _Container.Get<T>();
            //return ContainerContext.Current.Container.Get<T>();
        }

        T IComponentLocator.ResolveComponent<T>(string key)
        {
            return _Container.Get<T>(key);
            //return ContainerContext.Current.Container.Get<T>(key);
        }
    }
}
