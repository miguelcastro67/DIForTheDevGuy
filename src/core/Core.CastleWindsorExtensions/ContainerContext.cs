using Castle.Windsor;
using System;

namespace Core.CastleWindsorExtensions
{
    public class ContainerContext
    {
        static ContainerContext()
        {
            if (Current == null)
                Current = new ContainerContext();
        }

        public static ContainerContext Current { get; private set; }

        public IWindsorContainer Container { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
    }
}
