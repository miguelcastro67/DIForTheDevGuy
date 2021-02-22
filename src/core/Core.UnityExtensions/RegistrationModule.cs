using Core.Common;
using System;
using Unity;
using Unity.Extension;

namespace Core.UnityExtensions
{
    public class RegistrationModule : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<ITypeActivator, DiActivator>();
            Container.RegisterType<IComponentLocator, ComponentLocator>();
        }
    }
}