using Core.Common;
using System;

namespace Core.AspCoreExtensions
{
    public class DiActivator : ITypeActivator
    {
        T ITypeActivator.CreateInstance<T>(Type type)
        {
            IServiceProvider container = ContainerContext.Current.Container;

            T instance = container.GetService(type) as T;

            return instance;
        }
    }
}
