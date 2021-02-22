using Core.Common;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.AspCoreExtensions
{
    public class RegistrationModule : AspCoreDIModule
    {
        public override void Load(IServiceCollection services)
        {
            services.AddTransient<ITypeActivator, DiActivator>();
            services.AddTransient<IComponentLocator, ComponentLocator>();
        }
    }
}
