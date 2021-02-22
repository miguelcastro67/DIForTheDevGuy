using Core.Common;
using MoviePhile.Data;
using MoviePhile.Data.Abstractions;
using System.Linq;
using Unity;
using Unity.Extension;

namespace MoviePhile.UnityExtensions
{
    public class RegistrationModule : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IMovieRepository, MovieRepository>();
            Container.RegisterType<IActorRepository, ActorRepository>();
            Container.RegisterType<IGenreRepository, GenreRepository>();
            Container.RegisterSingleton<ILocalStrings, LocalStrings>();
        }
    }
}
