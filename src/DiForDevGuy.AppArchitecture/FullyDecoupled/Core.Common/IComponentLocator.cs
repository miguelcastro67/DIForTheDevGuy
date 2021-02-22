using System;

namespace Core.Common
{
    public interface IComponentLocator
    {
        T ResolveComponent<T>();
        T ResolveComponent<T>(string key);
    }
}
