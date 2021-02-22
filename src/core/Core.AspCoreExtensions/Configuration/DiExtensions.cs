using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.AspCoreExtensions.Configuration
{
    public static class DiExtensions
    {
        public static IServiceCollection RegisterModule(this IServiceCollection services, AspCoreDIModule module)
        {
            module.Load(services);

            return services;
        }
    }
}