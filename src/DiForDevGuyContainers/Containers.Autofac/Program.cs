using System;
using autofac = Autofac;
using Autofac;
using Lib;
using Lib.Abstractions;

namespace Containers.Autofac
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
                            Console.Write("Enter Avenger name: ");
                            string name = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(name))
                            {
                                autofac.ContainerBuilder builder = new autofac.ContainerBuilder();

                                builder.RegisterType<AvengerRepository>().As<IAvengerRepository>();
                                builder.RegisterType<Logger>().As<ILogger>();
                                builder.RegisterType<SuperheroService>();

                                IContainer container = builder.Build();

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
