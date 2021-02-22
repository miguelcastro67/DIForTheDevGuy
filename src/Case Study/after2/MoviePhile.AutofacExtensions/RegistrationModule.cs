using Autofac;
using Core.Common;
using MoviePhile.Data;
using System.Linq;

namespace MoviePhile.AutofacExtensions
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(MovieRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .As(t => t.GetInterfaces()?.FirstOrDefault(
                    i => i.Name == "I" + t.Name));

            builder.RegisterType<LocalStrings>().As<ILocalStrings>().SingleInstance();
        }
    }
}
