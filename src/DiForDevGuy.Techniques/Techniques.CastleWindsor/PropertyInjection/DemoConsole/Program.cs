using System;
using Castle.MicroKernel.Registration;
using Lib;
using Lib.Abstractions;
using windsor = Castle.Windsor;

namespace DemoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Castle Windsor - Property Injection");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("1 - Get all Avengers - standard property injection");
                Console.WriteLine("2 - Get all Avengers - post-construction resolve");
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
                            #region post-construction resolve

                            Console.WriteLine("Post-Construction Resolve");
                            Console.WriteLine();

                            windsor.WindsorContainer container = new windsor.WindsorContainer();

                            container.Register(Component.For<IAvengerRepository>().ImplementedBy<AvengerRepository>());
                            container.Register(Component.For<ILogger>().ImplementedBy<Logger>());
                            container.Register(Component.For<SuperheroService>());

                            SuperheroService superheroService = new SuperheroService();

                            // no direct support for this

                            // jump-through-hoops technique here:
                            // http://www.primordialcode.com/blog/post/castle-windsor-resolve-dependencies-existing-object-instance
                            //

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