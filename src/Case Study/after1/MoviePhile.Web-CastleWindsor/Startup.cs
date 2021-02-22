using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoviePhile.Data;
using MoviePhile.Data.Abstractions;
using System;
using System.Linq;

namespace MoviePhile.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc();

            IWindsorContainer container = new WindsorContainer();

            container.Register(Component.For<IMovieRepository>().ImplementedBy<MovieRepository>().LifestyleCustom<MsScopedLifestyleManager>());
            container.Register(Component.For<IGenreRepository>().ImplementedBy<GenreRepository>().LifestyleCustom<MsScopedLifestyleManager>());
            container.Register(Component.For<IActorRepository>().ImplementedBy<ActorRepository>().LifestyleCustom<MsScopedLifestyleManager>());

            IServiceProvider serviceProvider = WindsorRegistrationHelper.CreateServiceProvider(container, services);

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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
