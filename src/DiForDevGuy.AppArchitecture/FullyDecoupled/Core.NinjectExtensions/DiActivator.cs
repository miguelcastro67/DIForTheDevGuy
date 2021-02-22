using Core.Common;
using Ninject;
using System;

namespace Core.NinjectExtensions
{
    public class DiActivator : ITypeActivator
    {
        public DiActivator(IKernel container)
        {
            _Container = container;
        }

        IKernel _Container;

        T ITypeActivator.CreateInstance<T>(Type type)
        {
            //IKernel container = ContainerContext.Current.Container;

            T instance = _Container.Get(type) as T;

            return instance;
        }

        T ITypeActivator.CreateInstance<T>()
        {
            //IKernel container = ContainerContext.Current.Container;

            T instance = _Container.Get<T>() as T;

            return instance;
        }
    }
}
