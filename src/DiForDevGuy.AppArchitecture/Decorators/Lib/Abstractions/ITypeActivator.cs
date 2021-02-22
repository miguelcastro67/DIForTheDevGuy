using System;
using System.Collections.Generic;
using System.Text;

namespace Lib.Abstractions
{
    public interface ITypeActivator
    {
        T CreateInstance<T>(Type type) where T : class;
        T CreateInstance<T>() where T : class;
    }
}
