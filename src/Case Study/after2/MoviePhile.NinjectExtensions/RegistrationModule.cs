using Core.Common;
using Core.NinjectExtensions;
using MoviePhile.Data;
using MoviePhile.Data.Abstractions;
using Ninject.Modules;
using System.Linq;

namespace MoviePhile.NinjectExtensions
{
    public class RegistrationModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IMovieRepository>().To<MovieRepository>().InScope(x => LifetimeScope.Current);
            Kernel.Bind<IGenreRepository>().To<GenreRepository>().InScope(x => LifetimeScope.Current);
            Kernel.Bind<IActorRepository>().To<ActorRepository>().InScope(x => LifetimeScope.Current);
            Kernel.Bind<ILocalStrings>().To<LocalStrings>().InScope(x => LifetimeScope.Current);
        }
    }
}
