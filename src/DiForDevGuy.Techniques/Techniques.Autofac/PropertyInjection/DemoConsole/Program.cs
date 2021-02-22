using Autofac;
using Lib;
using Lib.Abstractions;
using System;
using autofac = Autofac;

namespace DemoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Autofac - Property Injection");
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

                            autofac.ContainerBuilder builder = new autofac.ContainerBuilder();

                            builder.RegisterType<AvengerRepository>().As<IAvengerRepository>();
                            builder.RegisterType<Logger>().As<ILogger>();
                            builder.RegisterType<SuperheroService>().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

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
                            #region post-construction resolve

                            Console.WriteLine("Post-Construction Resolve");
                            Console.WriteLine();

                            autofac.ContainerBuilder builder = new autofac.ContainerBuilder();

                            builder.RegisterType<AvengerRepository>().As<IAvengerRepository>();
                            builder.RegisterType<Logger>().As<ILogger>();
                            // note SuperheroService NOT registered this time

                            autofac.IContainer container = builder.Build();

                            SuperheroService superheroService = new SuperheroService();
                            container.InjectProperties(superheroService);

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

                            autofac.ContainerBuilder builder = new autofac.ContainerBuilder();

                            builder.RegisterType<AvengerRepository>().As<IAvengerRepository>();
                            builder.RegisterType<Logger>().As<ILogger>();
                            // no PropertyAutowired because still doing InjectProperties internally
                            builder.RegisterType<SuperheroService2>();
                            
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