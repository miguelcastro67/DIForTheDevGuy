using Lib;
using Lib.Abstractions;
using Ninject;
using System;

namespace DemoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Ninject - Instance Lifetime");
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

                            IKernel kernel = new StandardKernel();

                            kernel.Bind<IAvengerRepository>().To<AvengerRepository>();
                            kernel.Bind<ILogger>().To<Logger>();

                            SuperheroService superheroService = kernel.Get<SuperheroService>();

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

                            IKernel kernel = new StandardKernel();

                            kernel.Bind<IAvengerRepository>().To<AvengerRepository>();
                            kernel.Bind<ILogger>().To<Logger>();
                            kernel.Bind<SuperheroService>().To<SuperheroService>().InSingletonScope();
                          
                            bool exitSingleton = false;
                            while (!exitSingleton)
                            {
                                Console.Write("Press [Enter] to make call using existing cotainer, 0 to exit.");
                                string singletonChoice = Console.ReadLine();
                                switch (singletonChoice)
                                {
                                    case "":
                                        SuperheroService superheroService = kernel.Get<SuperheroService>();

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

                            IKernel kernel = new StandardKernel();

                            kernel.Bind<IAvengerRepository>().To<AvengerRepository>().InScope(x => LifetimeScope.Current);
                            kernel.Bind<ILogger>().To<Logger>().InScope(x => LifetimeScope.Current);
                            kernel.Bind<SuperheroService>().ToSelf().InScope(x => LifetimeScope.Current);

                            using (var myScope = new ScopeObject())
                            {
                                LifetimeScope.Current = myScope;

                                SuperheroService superheroService = kernel.Get<SuperheroService>();

                                var avengers = superheroService.GetAvengers();
                                Console.WriteLine();
                                foreach (var avenger in avengers)
                                {
                                    Console.WriteLine("{0}, who is really {1}, and has {2}.",
                                        avenger.SuperheroName, avenger.RealName, avenger.Power);
                                }
                            }

                            // should fall through here without error to prove "container" is still usable
                            SuperheroService superheroService2 = kernel.Get<SuperheroService>();
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
