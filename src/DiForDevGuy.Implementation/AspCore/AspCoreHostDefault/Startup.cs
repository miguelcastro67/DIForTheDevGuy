using AspCoreHostDefault.Core;
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
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient<IAvengerRepository, AvengerRepository>();
            services.AddTransient<ILogger, Logger>();
            services.AddScoped<LoggerMiddleware>();
            services.AddScoped<MyCustomFilter>();

            //services.AddTransient<IAvengerRepository, AvengerRepository>(); // different instance each time injection is resolved
            //services.AddSingleton<IAvengerRepository, AvengerRepository>(); // same instance across all requests
            //services.AddScoped<IAvengerRepository, AvengerRepository>(); // same instance for entire request 
            //services.AddDbContext<> // I really really hate it when I see this kind of design
        }
    }
}
