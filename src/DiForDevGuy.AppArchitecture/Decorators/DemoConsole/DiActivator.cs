using Autofac;
using Lib.Abstractions;
using System;

namespace DemoConsole
{
    public class DiActivator : ITypeActivator
    {
        public DiActivator(ILifetimeScope container)
        {
            _Container = container;
        }

        ILifetimeScope _Container;

        T ITypeActivator.CreateInstance<T>(Type type)
        {
            T instance = _Container.Resolve(type) as T;

            return instance;
        }

        T ITypeActivator.CreateInstance<T>()
        {
            T instance = _Container.Resolve<T>();

            return instance;
        }
    }
}
