using Core.Common;
using Ninject.Modules;
using System;

namespace Core.NinjectExtensions
{
    public class RegistrationModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<ITypeActivator>().To<DiActivator>();
            Kernel.Bind<IComponentLocator>().To<ComponentLocator>();
        }
    }
}
