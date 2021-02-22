using System;
using autofac = Autofac;
using Autofac;
using Lib;
using Lib.Abstractions;
using Autofac.Features.OwnedInstances;

namespace DemoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Autofac - Instance Lifetime");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("1 - Get all Avengers - conventional");
                Console.WriteLine("2 - Get all Avengers - singleton");
                Console.WriteLine("3 - Get all Avengers - scoped");
                Console.WriteLine("4 - Get all Avengers - owned scoped");
                Console.WriteLine("0 - Exit");
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        {
                            Console.WriteLine("Transient");
                            Console.WriteLine();

                            autofac.ContainerBuilder builder = new autofac.ContainerBuilder();

                            builder.RegisterType<AvengerRepository>().As<IAvengerRepository>();
                            builder.RegisterType<Logger>().As<ILogger>();
                            builder.RegisterType<SuperheroService>();

                            autofac.IContainer container = builder.Build();

                            SuperheroService superheroService = container.Resolve<SuperheroService>();

                            var avengers = superheroService.GetAvengers();
                            Console.WriteLine();
                            foreach (var avenger in avengers)
                            {
                                Console.WriteLine("{0}, who is really {1}, and has {2}.",
                                    avenger.SuperheroName, avenger.RealName, avenger.Power);
                            }
                        }
                        break;
                    case "2":
                        {
                            Console.WriteLine("Singleton");
                            Console.WriteLine();

                            autofac.ContainerBuilder builder = new autofac.ContainerBuilder();

                            builder.RegisterType<AvengerRepository>().As<IAvengerRepository>();
                            builder.RegisterType<Logger>().As<ILogger>();
                            builder.RegisterType<SuperheroService>().SingleInstance();

                            autofac.IContainer container = builder.Build();
                            
                            bool exitSingleton = false;
                            while (!exitSingleton)
                            {
                                Console.Write("Press [Enter] to make call using existing cotainer, 0 to exit.");
                                string singletonChoice = Console.ReadLine();
                                switch (singletonChoice)
                                {
                                    case "":
                                        SuperheroService superheroService = container.Resolve<SuperheroService>();

                                        var avengers = superheroService.GetAvengers();
                                        Console.WriteLine();
                                        foreach (var avenger in avengers)
                                        {
                                            Console.WriteLine("{0}, who is really {1}, and has {2}.",
                                                avenger.SuperheroName, avenger.RealName, avenger.Power);
                                        }
                                        break;
                                    case "0":
                                        exitSingleton = true;
                                        break;
                                }

                                Console.WriteLine();
                            }                            
                        }
                        break;
                    case "3":
                        {
                            Console.WriteLine("Scoped");
                            Console.WriteLine();

                            autofac.ContainerBuilder builder = new autofac.ContainerBuilder();
                            
                            builder.RegisterType<AvengerRepository>().As<IAvengerRepository>().InstancePerLifetimeScope();
                            builder.RegisterType<Logger>().As<ILogger>().InstancePerLifetimeScope();
                            builder.RegisterType<SuperheroService>().InstancePerLifetimeScope();

                            autofac.IContainer container = builder.Build();

                            using (ILifetimeScope lifetimeScope = container.BeginLifetimeScope())
                            {
                                SuperheroService superheroService = lifetimeScope.Resolve<SuperheroService>();

                                var avengers = superheroService.GetAvengers();
                                Console.WriteLine();
                                foreach (var avenger in avengers)
                                {
                                    Console.WriteLine("{0}, who is really {1}, and has {2}.",
                                        avenger.SuperheroName, avenger.RealName, avenger.Power);
                                }
                            }

                            // should fall through here without error to prove "container" is still usable
                            SuperheroService superheroService2 = container.Resolve<SuperheroService>();
                        }
                        break;
                    case "4":
                        {
                            Console.WriteLine("Owned Scope");
                            Console.WriteLine();

                            autofac.ContainerBuilder builder = new autofac.ContainerBuilder();

                            builder.RegisterType<AvengerRepository>().As<IAvengerRepository>().InstancePerLifetimeScope();
                            builder.RegisterType<Logger>().As<ILogger>().InstancePerLifetimeScope();
                            builder.RegisterType<SuperheroService>().InstancePerLifetimeScope();

                            autofac.IContainer container = builder.Build();

                            using (Owned<SuperheroService> ownedSuperheroService = container.Resolve<Owned<SuperheroService>>())
                            {
                                var avengers = ownedSuperheroService.Value.GetAvengers();

                                Console.WriteLine();
                                foreach (var avenger in avengers)
                                {
                                    Console.WriteLine("{0}, who is really {1}, and has {2}.",
                                        avenger.SuperheroName, avenger.RealName, avenger.Power);
                                }
                            }

                            // should fall through here without error to prove "container" is still usable
                            SuperheroService superheroService2 = container.Resolve<SuperheroService>();
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