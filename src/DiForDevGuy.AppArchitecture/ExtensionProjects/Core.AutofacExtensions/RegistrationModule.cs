using Autofac;
using Core.Common;
using System;

namespace Core.AutofacExtensions
{
    public class RegistrationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DiActivator>().As<ITypeActivator>();
            builder.RegisterType<ComponentLocator>().As<IComponentLocator>();
        }
    }
}
