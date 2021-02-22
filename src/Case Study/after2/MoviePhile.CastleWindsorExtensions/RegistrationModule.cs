using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Core.Common;
using MoviePhile.Data;
using MoviePhile.Data.Abstractions;
using System.Linq;

namespace MoviePhile.CastleWindsorExtensions
{
    public class RegistrationModule : IWindsorInstaller
    {
        void IWindsorInstaller.Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IMovieRepository>().ImplementedBy<MovieRepository>());
            container.Register(Component.For<IActorRepository>().ImplementedBy<ActorRepository>());
            container.Register(Component.For<IGenreRepository>().ImplementedBy<GenreRepository>());
            container.Register(Component.For<ILocalStrings>().ImplementedBy<LocalStrings>().LifestyleSingleton());
        }
    }
}
