using Autofac;
using System;

namespace Core.AutofacExtensions
{
    public class ContainerContext
    {
        static ContainerContext()
        {
            if (Current == null)
                Current = new ContainerContext();
        }

        public static ContainerContext Current { get; private set; }

        public IContainer Container { get; set; }
    }
}
