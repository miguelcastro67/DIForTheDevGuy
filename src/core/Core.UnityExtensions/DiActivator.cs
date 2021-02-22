using Core.Common;
using System;
using Unity;

namespace Core.UnityExtensions
{
    public class DiActivator : ITypeActivator
    {
        T ITypeActivator.CreateInstance<T>(Type type)
        {
            IUnityContainer container = ContainerContext.Current.Container;

            T instance = container.Resolve(type) as T;

            return instance;
        }
    }
}
