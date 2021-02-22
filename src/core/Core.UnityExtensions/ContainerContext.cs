using System;
using Unity;

namespace Core.UnityExtensions
{
    public class ContainerContext
    {
        static ContainerContext()
        {
            if (Current == null)
                Current = new ContainerContext();
        }

        public static ContainerContext Current { get; private set; }

        public IUnityContainer Container { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
    }
}
