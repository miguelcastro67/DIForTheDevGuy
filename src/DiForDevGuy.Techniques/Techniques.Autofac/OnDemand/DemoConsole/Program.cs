using Autofac;
using autofac = Autofac;
using Lib;
using Lib.Abstractions;
using System;

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
                Console.WriteLine("Autofac - On-Demand Resolving");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("1 - Get all Avengers - on-demand");
                Console.WriteLine("0 - Exit");
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        {
                            Console.WriteLine("On-Demand");
                            Console.WriteLine();

                            autofac.ContainerBuilder builder = new autofac.ContainerBuilder();

                            builder.RegisterType<ComponentLocator>().As<IComponentLocator>();
                            builder.RegisterType<AvengerRepository>().As<IAvengerRepository>();
                            builder.RegisterType<Logger>().As<ILogger>();
                            builder.RegisterType<SuperheroService>();

                            Container = builder.Build();

                            SuperheroService superheroService = Container.Resolve<SuperheroService>();

                            var avengers = superheroService.GetAvengers();
                            Console.WriteLine();
                            foreach (var avenger in avengers)
                            {
                                Console.WriteLine("{0}, who is really {1}, and has {2}.",
                                    avenger.SuperheroName, avenger.RealName, avenger.Power);
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