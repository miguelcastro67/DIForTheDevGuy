using System;

namespace Core.AspCoreExtensions
{
    public class ContainerContext
    {
        static ContainerContext()
        {
            if (Current == null)
                Current = new ContainerContext();
        }

        public static ContainerContext Current { get; private set; }

        public IServiceProvider Container { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
    }
}
 