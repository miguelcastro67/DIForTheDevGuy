using Autofac;
using Lib;
using System;

namespace DemoConsole
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
            //return Program.Container.Resolve<T>();
        }
    }
}
