using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.AspCoreExtensions
{
    public abstract class AspCoreDIModule
    {
        public abstract void Load(IServiceCollection services);
    }
}
