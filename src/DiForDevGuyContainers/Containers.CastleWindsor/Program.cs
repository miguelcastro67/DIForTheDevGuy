using System;
using windsor = Castle.Windsor;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Lib;
using Lib.Abstractions;

namespace Containers.CastleWindsor
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("1 - Get all Avengers");
                Console.WriteLine("2 - Get a single Avenger");
                Console.WriteLine("0 - Exit");
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        {
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
                            Console.Write("Enter Avenger name: ");
                            string name = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(name))
                            {
                                windsor.WindsorContainer container = new windsor.WindsorContainer();

                                container.Register(Component.For<IAvengerRepository>().ImplementedBy<AvengerRepository>());
                                container.Register(Component.For<ILogger>().ImplementedBy<Logger>());
                                container.Register(Component.For<SuperheroService>());

                                SuperheroService superheroService = container.Resolve<SuperheroService>();

                                var avenger = superheroService.GetAvenger(name);
                                if (avenger != null)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("{0}, who is really {1}, and has {2}.",
                                        avenger.SuperheroName, avenger.RealName, avenger.Power);
                                }
                                else
                                    Console.WriteLine("Cannot find {0} Avenger.");
                            }
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
