using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.Common
{
    public interface IContainerManager
    {
        void BuildContainer();
        void BuildContainer(IApplicationBuilder appBuilder);
        void BuildContainer(IServiceCollection services);
        IServiceProvider GetServiceProvider();
        IComponentLocator GetLocator();
    }
}
