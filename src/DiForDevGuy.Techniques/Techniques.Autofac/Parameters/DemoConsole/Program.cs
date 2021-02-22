using Autofac;
using Autofac.Core;
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
                Console.WriteLine("Autofac - Parameter Resolve Techniques");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("1 - Get single Avenger - named parameter");
                Console.WriteLine("2 - Get all Avengers - typed parameter");
                Console.WriteLine("3 - Get all Avengers - resolved parameter");
                Console.WriteLine("0 - Exit");
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        {
                            #region named parameter

                            Console.WriteLine("Named Parameter");
                            Console.WriteLine();

                            autofac.ContainerBuilder builder = new autofac.ContainerBuilder();

                            // register everything and key-register handlers

                            builder.RegisterType<Logger>().As<ILogger>();

                            builder.RegisterAssemblyTypes(typeof(SuperheroService).Assembly)
                                .Where(t => t.Name.EndsWith("Repository"))
                                .As(t => t.GetInterfaces()?.FirstOrDefault(
                                    i => i.Name == "I" + t.Name));

                            builder.RegisterAssemblyTypes(typeof(SuperheroService).Assembly)
                                .Where(t => t.Name.EndsWith("Handler"))
                                .As<IAvengerHandler>()
                                .Keyed<IAvengerHandler>(t =>
                                {
                                    string key = t.Name.Replace("Handler", "").ToLower();
                                    return key;
                                });
                           
                            // register parameters by name - will be injecting ILifetimeScope into service and resolving by key
                            
                            builder.RegisterType<SuperheroService>()
                               .WithParameter(
                                 new NamedParameter("avengerName", "ironman"));

                            Container = builder.Build();
                            
                            SuperheroService superheroService = Container.Resolve<SuperheroService>();

                            var avenger = superheroService.GetAvenger();
                            Console.WriteLine();

                            Console.WriteLine("{0}, who is really {1}, and has {2}.",
                                avenger.SuperheroName, avenger.RealName, avenger.Power);

                            #endregion
                        }
                        break;
                    case "2":
                        {
                            #region typed parameter

                            Console.WriteLine("Typed Parameter");
                            Console.WriteLine();

                            autofac.ContainerBuilder builder = new autofac.ContainerBuilder();

                            // register everything and key-register handlers

                            builder.RegisterType<Logger>().As<ILogger>();

                            builder.RegisterAssemblyTypes(typeof(SuperheroService).Assembly)
                                .Where(t => t.Name.EndsWith("Repository"))
                                .As(t => t.GetInterfaces()?.FirstOrDefault(
                                    i => i.Name == "I" + t.Name));
                            
                            builder.RegisterAssemblyTypes(typeof(SuperheroService).Assembly)
                                .Where(t => t.Name.EndsWith("Handler"))
                                .As<IAvengerHandler>()
                                .Keyed<IAvengerHandler>(t =>
                                {
                                    string key = t.Name.Replace("Handler", "").ToLower();
                                    return key;
                                });

                            // register parameters by type - will be injecting ILifetimeScope into service and resolving by key

                            builder.RegisterType<SuperheroService2>()
                               .WithParameter(
                                 new TypedParameter(typeof(string), "hulk"));
                            
                            Container = builder.Build();
                            
                            SuperheroService2 superheroService = Container.Resolve<SuperheroService2>();

                            var avenger = superheroService.GetAvenger();
                            Console.WriteLine();

                            Console.WriteLine("{0}, who is really {1}, and has {2}.",
                                avenger.SuperheroName, avenger.RealName, avenger.Power);

                            #endregion
                        }
                        break;
                    case "3":
                        {
                            #region resolved parameter

                            Console.WriteLine("Resolved parameter");
                            Console.WriteLine();

                            autofac.ContainerBuilder builder = new autofac.ContainerBuilder();
                            
                            // register everything and key-register handlers

                            builder.RegisterType<Logger>().As<ILogger>();

                            builder.RegisterAssemblyTypes(typeof(SuperheroService).Assembly)
                                .Where(t => t.Name.EndsWith("Repository"))
                                .As(t => t.GetInterfaces()?.FirstOrDefault(
                                    i => i.Name == "I" + t.Name));

                            builder.RegisterAssemblyTypes(typeof(SuperheroService).Assembly)
                                .Where(t => t.Name.EndsWith("Handler"))
                                .As<IAvengerHandler>()
                                .Keyed<IAvengerHandler>(t =>
                                {
                                    string key = t.Name.Replace("Handler", "").ToLower();
                                    return key;
                                });

                            // can use Container now because a lambda makes it defered until later
                            
                            builder.RegisterType<SuperheroService3>()
                               .WithParameter(
                                 new ResolvedParameter(
                                   (pi, ctx) => pi.ParameterType == typeof(IAvengerHandler),
                                   // can probably get creative here with more logic to determine which Avenger
                                   (pi, ctx) => ctx.ResolveKeyed<IAvengerHandler>("captainamerica")));

                            Container = builder.Build();

                            SuperheroService3 superheroService = Container.Resolve<SuperheroService3>();

                            var avenger = superheroService.GetAvenger();
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