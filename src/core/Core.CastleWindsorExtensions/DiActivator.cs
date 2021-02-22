using Castle.Windsor;
using Core.Common;
using System;

namespace Core.CastleWindsorExtensions
{
    public class DiActivator : ITypeActivator
    {
        T ITypeActivator.CreateInstance<T>(Type type)
        {
            IWindsorContainer container = ContainerContext.Current.Container;

            T instance = container.Resolve(type) as T;

            return instance;
        }
    }
}