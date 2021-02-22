using System;

namespace Core.Common
{
    public interface IContainerManager
    {
        void BuildContainer();
        IComponentLocator GetLocator();
    }
}
