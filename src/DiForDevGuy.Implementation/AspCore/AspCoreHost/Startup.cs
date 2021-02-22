using AspCoreHost.Core;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Lib;
using Lib.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AspCoreHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMiddleware<LoggerMiddleware>()
               .UseMvc();
        }

        public IConfiguration Configuration { get; }

        #region alternative way to do it - without AddAutofac in the host set up
        // public IServiceProvider ConfigureServices(IServiceCollection services)
        // {
        //     services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        //     // for built-in DI
        //     //IServiceProvider serviceProvider = services.BuildServiceProvider();

        //     // for autofac DI
        //     ContainerBuilder builder = new ContainerBuilder();

        //     builder.Populate(services); // registers all the default/built-in services/controllers

        //     builder.RegisterType<AvengerRepository>().As<IAvengerRepository>().InstancePerLifetimeScope();
        //     builder.RegisterType<Logger>().As<ILogger>().InstancePerLifetimeScope();
        //
        //     IContainer container = builder.Build();

        //     IServiceProvider serviceProvider = new AutofacServiceProvider(container);

        //     return serviceProvider;
        // }
        #endregion

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
        
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Notice that InstancePerRequest is not supported for Autofac integration.
            // Instead it supports InstancePerLifetimeScope as with normal lifetime scoping as seen in previous demos.
            builder.RegisterType<AvengerRepository>().As<IAvengerRepository>().InstancePerLifetimeScope();
            builder.RegisterType<Logger>().As<ILogger>().InstancePerLifetimeScope();
            builder.RegisterType<LoggerMiddleware>().InstancePerLifetimeScope();
            builder.RegisterType<MyCustomFilter>().InstancePerLifetimeScope();

            // notice controller registration happens automatically
        }
    }
}
