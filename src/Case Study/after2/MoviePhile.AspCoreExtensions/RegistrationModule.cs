using Core.AspCoreExtensions;
using Core.Common;
using Microsoft.Extensions.DependencyInjection;
using MoviePhile.Data;
using MoviePhile.Data.Abstractions;
using System.Linq;

namespace MoviePhile.AspCoreExtensions
{
    public class RegistrationModule : AspCoreDIModule       
    {
        public override void Load(IServiceCollection services)
        {
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddTransient<ILocalStrings, LocalStrings>();
        }
    }
}
