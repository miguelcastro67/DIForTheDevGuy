using Core.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;

namespace MoviePhile.Web
{
    public class Startup
    {       
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            ConfigurationManager.RefreshSection("appSettings");
            Type containerManagerType = Type.GetType(ConfigurationManager.AppSettings["containerManager"]);
            if (containerManagerType != null)
            {
                _ContainerManager = Activator.CreateInstance(containerManagerType) as IContainerManager;
            }
        }
        
        public IConfiguration Configuration { get; }
        IContainerManager _ContainerManager { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            IServiceProvider serviceProvider = null;

            if (_ContainerManager != null)
            {
                _ContainerManager.BuildContainer(services);

                serviceProvider = _ContainerManager.GetServiceProvider();
            }

            return serviceProvider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                    context.Context.Response.Headers.Add("Expires", "-1");
                }
            });
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            if (_ContainerManager != null)
                _ContainerManager.BuildContainer(app);
        }
    }
}
