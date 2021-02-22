using Autofac;
using Core.Common;
using System;

namespace Core.AutofacExtensions
{
    public class DiActivator : ITypeActivator
    {
        T ITypeActivator.CreateInstance<T>(Type type)
        {
            IContainer container = ContainerContext.Current.Container;

            T instance = container.Resolve(type) as T;

            return instance;
        }
    }
}
