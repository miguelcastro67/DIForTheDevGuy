using Autofac;
using Core.Common;
using System;

namespace Core.AutofacExtensions
{
    public class ComponentLocator : IComponentLocator
    {
        public ComponentLocator(ILifetimeScope container)
        {
            _Container = container;
        }

        ILifetimeScope _Container;

        T IComponentLocator.ResolveComponent<T>()
        {
            return _Container.Resolve<T>();
        }

        T IComponentLocator.ResolveComponent<T>(string key)
        {
            return _Container.ResolveKeyed<T>(key);
        }
    }
}
