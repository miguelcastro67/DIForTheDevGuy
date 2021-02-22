using System;

namespace Lib.Abstractions
{
    public interface IComponentLocator
    {
        T ResolveComponent<T>();
        T ResolveComponent<T>(string key);
    }
}
