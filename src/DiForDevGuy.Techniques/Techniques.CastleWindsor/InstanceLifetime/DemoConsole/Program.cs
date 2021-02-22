using Lib;
using Lib.Abstractions;
using windsor = Castle.Windsor;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using System;
using Castle.MicroKernel.Lifestyle;

namespace DemoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Castle Windsor - Instance Lifetime");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("1 - Get all Avengers - conventional");
                Console.WriteLine("2 - Get all Avengers - singleton");
                Console.WriteLine("3 - Get all Avengers - scoped");
                Console.WriteLine("0 - Exit");
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        {
                            Console.WriteLine("Transient");
                            Console.WriteLine();

                            windsor.WindsorContainer container = new windsor.WindsorContainer();

                            container.Register(Component.For<IAvengerRepository>().ImplementedBy<AvengerRepository>());
                            container.Register(Component.For<ILogger>().ImplementedBy<Logger>());
                            container.Register(Component.For<SuperheroService>());

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

                            windsor.WindsorContainer container = new windsor.WindsorContainer();

                            container.Register(Component.For<IAvengerRepository>().ImplementedBy<AvengerRepository>());
                            container.Register(Component.For<ILogger>().ImplementedBy<Logger>());
                            container.Register(Component.For<SuperheroService>().LifestyleSingleton());

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

                            windsor.IWindsorContainer container = new windsor.WindsorContainer();

                            container.Register(Component.For<IAvengerRepository>().ImplementedBy<AvengerRepository>().LifestyleScoped());
                            container.Register(Component.For<ILogger>().ImplementedBy<Logger>().LifestyleScoped());
                            container.Register(Component.For<SuperheroService>().LifestyleScoped());

                            container.BeginScope();

                            using (IDisposable scope = container.BeginScope())
                            {
                                SuperheroService superheroService = container.Resolve<SuperheroService>();

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
                    case "0":
                        exit = true;
                        break;
                }

                Console.WriteLine();
            }
        }
    }

}
