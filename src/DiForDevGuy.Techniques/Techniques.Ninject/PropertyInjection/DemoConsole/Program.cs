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
                Console.WriteLine("Ninject - Property Injection");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("1 - Get all Avengers - standard property injection");
                Console.WriteLine("2 - Get all Avengers - post-construction resolve");
                Console.WriteLine("3 - Get all Avengers - post-construction resolve (internal)");
                Console.WriteLine("0 - Exit");
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        {
                            #region standard property injection

                            Console.WriteLine("Standard Property Injection");
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
                            #region post-construction resolve

                            Console.WriteLine("Post-Construction Resolve");
                            Console.WriteLine();

                            IKernel container = new StandardKernel();

                            container.Bind<IAvengerRepository>().To<AvengerRepository>();
                            container.Bind<ILogger>().To<Logger>();

                            SuperheroService superheroService = new SuperheroService();
                            container.Inject(superheroService);

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
                    case "3":
                        {
                            #region post-construction resolve (internal)

                            Console.WriteLine("Post-Construction Resolve (internal)");
                            Console.WriteLine();

                            IKernel container = new StandardKernel();

                            container.Bind<IAvengerRepository>().To<AvengerRepository>();
                            container.Bind<ILogger>().To<Logger>();

                            SuperheroService2 superheroService = container.Get<SuperheroService2>();

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