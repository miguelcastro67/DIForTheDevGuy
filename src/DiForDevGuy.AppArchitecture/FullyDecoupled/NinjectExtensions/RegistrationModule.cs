using Lib;
using Lib.Abstractions;
using Lib.Handlers;
using Ninject.Modules;
using System;
using System.Linq;

namespace NinjectExtensions
{
    public class RegistrationModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IConfigurationFactory>().To<ConfigurationFactory>().InSingletonScope();
            Kernel.Bind<IAvengerRepository>().To<AvengerRepository>();
            
            Kernel.Bind<ILogger>().To<ConsoleLogger>();
            Kernel.Bind<ILogger>().To<TraceLogger>();

            Kernel.Bind<IAvengerHandler>().To<IronmanHandler>().Named("ironman");
            Kernel.Bind<IAvengerHandler>().To<ThorHandler>().Named("thor");
            Kernel.Bind<IAvengerHandler>().To<HulkHandler>().Named("hulk");
            Kernel.Bind<IAvengerHandler>().To<CaptainAmericaHandler>().Named("captainamerica");
            Kernel.Bind<IAvengerHandler>().To<BlackWidowHandler>().Named("blackwidow");


            //builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource(t =>
            //{
            //    return t.GetInterfaces().FirstOrDefault(i =>
            //        i.Name == "ILogger"
            //    ) != null;
            //})); 

            //builder.RegisterAssemblyTypes(typeof(SuperheroService).Assembly)
            //    .Where(t => t.Name.EndsWith("Handler"))
            //    .As<IAvengerHandler>()
            //    .Keyed<IAvengerHandler>(t =>
            //    {
            //        string key = t.Name.Replace("Handler", "").ToLower();
            //        return key;
            //    });
        }
    }
}
