using Autofac;
using Autofac.Features.ResolveAnything;
using Core.AutofacExtensions;
using Lib;
using Lib.Abstractions;
using System;
using System.Linq;
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
                Console.WriteLine("Autofac - Extension Project");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("1 - Get all Avengers - static-class wrapper");
                Console.WriteLine("2 - Get single Avenger - keyed-resolve wrapper");
                Console.WriteLine("0 - Exit");
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        {
                            #region static-class wrapper

                            Console.WriteLine("Static-Class Wrapper");
                            Console.WriteLine();

                            autofac.ContainerBuilder builder = new autofac.ContainerBuilder();

                            builder.RegisterModule<RegistrationModule>(); // this comes from extensions project
                            builder.RegisterType<ConfigurationFactory>().As<IConfigurationFactory>().SingleInstance();
                            builder.RegisterType<AvengerRepository>().As<IAvengerRepository>();
                            builder.RegisterType<SuperheroService>();

                            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource(t =>
                            {
                                return t.GetInterfaces().FirstOrDefault(i =>
                                    i.Name == "ILogger"
                                ) != null;
                            }));

                            ContainerContext.Current.Container = builder.Build();

                            SuperheroService superheroService = ContainerContext.Current.Container.Resolve<SuperheroService>();
                            
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
                            #region keyed-resolve wrapper
                            
                            Console.WriteLine("Keyed-Resolve Wrapper");
                            Console.WriteLine();

                            autofac.ContainerBuilder builder = new autofac.ContainerBuilder();

                            builder.RegisterModule<RegistrationModule>();
                            builder.RegisterType<ConfigurationFactory>().As<IConfigurationFactory>().SingleInstance();
                            builder.RegisterType<AvengerRepository>().As<IAvengerRepository>();
                            builder.RegisterType<SuperheroService>();

                            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource(t =>
                            {
                                return t.GetInterfaces().FirstOrDefault(i =>
                                    i.Name == "ILogger"
                                ) != null;
                            }));

                            builder.RegisterAssemblyTypes(typeof(SuperheroService).Assembly)
                                .Where(t => t.Name.EndsWith("Handler"))
                                .As<IAvengerHandler>()
                                .Keyed<IAvengerHandler>(t =>
                                {
                                    string key = t.Name.Replace("Handler", "").ToLower();
                                    return key;
                                });

                            ContainerContext.Current.Container = builder.Build();

                            Console.Write("Enter avenger name: ");
                            string name = Console.ReadLine();
                            if (name != "")
                            {
                                SuperheroService superheroService = ContainerContext.Current.Container.Resolve<SuperheroService>();

                                var avenger = superheroService.GetAvenger(name);
                                if (avenger != null)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("{0}, who is really {1}, and has {2}.",
                                        avenger.SuperheroName, avenger.RealName, avenger.Power);
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Avenger not found.");
                                }
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
