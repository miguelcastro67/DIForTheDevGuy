using System;

namespace Lib
{
    public interface IComponentLocator
    {
        T ResolveComponent<T>(params object[] args);
    }
}
