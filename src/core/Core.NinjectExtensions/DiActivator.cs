using Core.Common;
using Ninject;
using System;

namespace Core.NinjectExtensions
{
    public class DiActivator : ITypeActivator
    {
        T ITypeActivator.CreateInstance<T>(Type type)
        {
            IKernel container = ContainerContext.Current.Container;

            T instance = container.Get(type) as T;

            return instance;
        }
    }
}
