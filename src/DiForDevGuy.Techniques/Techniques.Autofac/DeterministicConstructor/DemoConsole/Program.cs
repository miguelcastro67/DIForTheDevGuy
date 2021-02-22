using System;
using autofac = Autofac;
using Autofac;
using Lib;
using Lib.Abstractions;
using Ext;

namespace DemoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Autofac - Deterministic Constructor");
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

                            #endregion
                        }
                        break;
                    case "2":
                        {
                            #region deterministic constructor injection

                            Console.WriteLine("Deterministic Constructor Injection");
                            Console.WriteLine();

                            autofac.ContainerBuilder builder = new autofac.ContainerBuilder();

                            builder.RegisterType<AvengerRepository>().As<IAvengerRepository>();
                            builder.RegisterType<Logger>().As<ILogger>();
                            //builder.RegisterType<SuperheroService2>();
                            builder.RegisterType<SuperheroService2>().FindConstructorsWith(new AwesomeConstructorFinder());

                            autofac.IContainer container = builder.Build();

                            SuperheroService2 superheroService = container.Resolve<SuperheroService2>();

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