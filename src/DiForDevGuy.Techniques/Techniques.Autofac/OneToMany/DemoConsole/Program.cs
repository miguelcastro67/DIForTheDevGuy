using Autofac;
using Lib;
using Lib.Abstractions;
using System;
using System.Linq;
using autofac = Autofac;

namespace DemoConsole
{
    class Program
    {
        public static autofac.IContainer Container { get; private set; }
        
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Autofac - One-To-Many Techniques");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("1 - Get all Avengers - one-to-many collection resolve");
                Console.WriteLine("2 - Get all Avengers - one-to-many keyed resolve");
                Console.WriteLine("0 - Exit");
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        {
                            #region one-to-many collection resolve

                            Console.WriteLine("One-to-Many collection resolve");
                            Console.WriteLine();

                            autofac.ContainerBuilder builder = new autofac.ContainerBuilder();

                            builder.RegisterType<SuperheroService>();
                            builder.RegisterType<Logger>().As<ILogger>();

                            builder.RegisterAssemblyTypes(typeof(SuperheroService).Assembly)
                                .Where(t => t.Name.EndsWith("Repository"))
                                .As(t => t.GetInterfaces()?.FirstOrDefault(
                                    i => i.Name == "I" + t.Name));
                            
                            // register all classes in assembly that end with Handler to the same interface
                            builder.RegisterAssemblyTypes(typeof(SuperheroService).Assembly)
                                .Where(t => t.Name.EndsWith("Handler"))
                                .As<IAvengerHandler>();

                            Container = builder.Build();
                            
                            SuperheroService superheroService = Container.Resolve<SuperheroService>();

                            // gets the list of avenger handlers that were injected
                            var avengers = superheroService.GetAvengers();

                            Console.WriteLine();
                            foreach (var avenger in avengers)
                            {
                                Console.WriteLine("{0}, who is really {1}, and has {2}.",
                                    avenger.SuperheroName, avenger.RealName, avenger.Power);
                            }

                            #endregion
                        }
                        break;
                    case "2":
                        {
                            #region one-to-many keyed resolve

                            Console.WriteLine("One-to-Many keyed resolve");
                            Console.WriteLine();
                            
                            autofac.ContainerBuilder builder = new autofac.ContainerBuilder();

                            builder.RegisterType<SuperheroService>();
                            builder.RegisterType<Logger>().As<ILogger>();
                            
                            builder.RegisterAssemblyTypes(typeof(SuperheroService).Assembly)
                                .Where(t => t.Name.EndsWith("Repository"))
                                .As(t => t.GetInterfaces()?.FirstOrDefault(
                                    i => i.Name == "I" + t.Name));
                            
                            // one-by-one
                            //builder.RegisterType<IronmanHandler>().As<IAvengerHandler>().Keyed<IAvengerHandler>("ironman");
                            //builder.RegisterType<ThorHandler>().As<IAvengerHandler>().Keyed<IAvengerHandler>("thor");
                            //builder.RegisterType<CaptainAmericaHandler>().As<IAvengerHandler>().Keyed<IAvengerHandler>("captainamerica");
                            //builder.RegisterType<HulkHandler>().As<IAvengerHandler>().Keyed<IAvengerHandler>("hulk");
                            //builder.RegisterType<BlackWidowHandler>().As<IAvengerHandler>().Keyed<IAvengerHandler>("blackwidow");
                            //builder.RegisterType<SpidermanHandler>().As<IAvengerHandler>().Keyed<IAvengerHandler>("spiderman");
                            
                            // combined with assembly-scanning
                            builder.RegisterAssemblyTypes(typeof(SuperheroService).Assembly)
                                .Where(t => t.Name.EndsWith("Handler"))
                                .As<IAvengerHandler>()
                                .Keyed<IAvengerHandler>(t =>
                                {
                                    string key = t.Name.Replace("Handler", "").ToLower();
                                    return key;
                                });
                            
                            Container = builder.Build();

                            // in production design, this can be wrapped in an extensions project (more about that later)
                            // ex: handlerLocator.GetHandler("ironman")
                            IAvengerHandler ironmanHandler = Container.ResolveKeyed<IAvengerHandler>("ironman");

                            SuperheroService superheroService = Container.Resolve<SuperheroService>();
                            
                            var avenger = superheroService.GetAvenger(ironmanHandler);
                            Console.WriteLine();

                            Console.WriteLine("{0}, who is really {1}, and has {2}.",
                                avenger.SuperheroName, avenger.RealName, avenger.Power);

                            #endregion
                        }
                        break;
                    case "0":
                        exit = true;
                        break;
                }
                
                Console.WriteLine();
            }
        }
    }
}