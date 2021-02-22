using System;
using Lib;
using Lib.Abstractions;
using Ninject;
using Ninject.Parameters;

namespace DemoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Ninject - Deterministic Constructor");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("1 - Get all Avengers - standard constructor injection");
                Console.WriteLine("2 - Get all Avengers - deterministic constructor injection");
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

                            IKernel container = new StandardKernel();

                            container.Bind<IAvengerRepository>().To<AvengerRepository>();
                            container.Bind<ILogger>().To<Logger>();

                            SuperheroService superheroService = container.Get<SuperheroService>();

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
                            #region deterministic constructor injection

                            Console.WriteLine("Deterministic Constructor Injection");
                            Console.WriteLine();

                            IKernel container = new StandardKernel();

                            container.Bind<IAvengerRepository>().To<AvengerRepository>();
                            container.Bind<ILogger>().To<Logger>();

                            SuperheroService2 superheroService = container.Get<SuperheroService2>(new ConstructorArgument("value1", "miguel"));

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