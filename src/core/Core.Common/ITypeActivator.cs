using System;

namespace Core.Common
{
    public interface ITypeActivator
    {
        T CreateInstance<T>(Type type) where T : class;
    }
}
