using Ninject;
using System;

namespace Core.NinjectExtensions
{
    public class ContainerContext
    {
        static ContainerContext()
        {
            if (Current == null)
                Current = new ContainerContext();
        }

        public static ContainerContext Current { get; private set; }

        public IKernel Container { get; set; }
    }
}
