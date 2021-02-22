using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoviePhile.Data;
using MoviePhile.Data.Abstractions;
using MoviePhile.Web.Ninject;
using Ninject;
using Ninject.Activation;
using Ninject.Infrastructure.Disposal;
using System;
using System.Linq;
using System.Threading;

namespace MoviePhile.Web
{
    public class Startup
    {
        private readonly AsyncLocal<Scope> _ScopeProvider = new AsyncLocal<Scope>();
        private IKernel _Kernel { get; set; }
        private object _Resolve(Type type) => _Kernel.Get(type);
        private object _RequestScope(IContext context) => _ScopeProvider.Value;
        private sealed class Scope : DisposableObject { }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // these extension methods wire ASP Core DI into Ninject DI
            services.AddRequestScopingMiddleware(() => _ScopeProvider.Value = new Scope());
            services.AddCustomControllerActivation(_Resolve);
            services.AddCustomViewComponentActivation(_Resolve);
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

            _Kernel = RegisterApplicationComponents(app);
        }

        private IKernel RegisterApplicationComponents(IApplicationBuilder app)
        {
            var kernel = new StandardKernel();
            
            // Register application services
            foreach (var ctrlType in app.GetControllerTypes())
            {
                kernel.Bind(ctrlType).ToSelf().InScope(_RequestScope);
            }

            // This is where our bindings are configurated
            kernel.Bind<IMovieRepository>().To<MovieRepository>().InScope(_RequestScope);
            kernel.Bind<IGenreRepository>().To<GenreRepository>().InScope(_RequestScope);
            kernel.Bind<IActorRepository>().To<ActorRepository>().InScope(_RequestScope);

            // Cross-wire required framework services
            kernel.BindToMethod(app.GetRequestService<IViewBufferScope>);

            return kernel;
        }
    }
}
