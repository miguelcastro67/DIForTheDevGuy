using Castle.MicroKernel;
using System;
using Lib;
using Lib.Abstractions;
using windsor = Castle.Windsor;
using Castle.MicroKernel.Registration;

namespace DemoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Castle Windsor - Deterministic Constructor Injection");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("1 - Get all Avengers - standard constructor injection");
                Console.WriteLine("2 - Get all Avengers - decorator pattern");
                Console.WriteLine("0 - Exit");
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        {
                            #region standard constructor injection

                            Console.WriteLine("Standard Constructor Injection");
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

                            #endregion
                        }
                        break;
                    case "2":
                        {
                            #region decorator pattern

                            Console.WriteLine("Decorator Pattern");
                            Console.WriteLine();

                            windsor.WindsorContainer container = new windsor.WindsorContainer();

                            container.Register(Component.For<IAvengerRepository>().ImplementedBy<AvengerRepository>());
                            container.Register(Component.For<ILogger>().ImplementedBy<Logger>());
                            container.Register(Component.For<DecoratedSuperheroService>());

                            DecoratedSuperheroService superheroService = container.Resolve<DecoratedSuperheroService>();

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
                    case "0":
                        exit = true;
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}