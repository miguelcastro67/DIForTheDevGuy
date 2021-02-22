using Autofac;
using Autofac.Configuration;
using Ext;
using Ext.Configuration;
using Lib;
using Lib.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Reflection;
using autofac = Autofac;

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
                Console.WriteLine("Autofac - Registration Techniques");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("1 - Get all Avengers - conventional");
                Console.WriteLine("2 - Get all Avengers - convention-based (simple)");
                Console.WriteLine("3 - Get all Avengers - convention-based (complex)");
                Console.WriteLine("4 - Get all Avengers - attribute-based");
                Console.WriteLine("5 - Get all Avengers - module-based");
                Console.WriteLine("6 - Get all Avengers - configuration-based (legacy)");
                Console.WriteLine("7 - Get all Avengers - configuration-based (new)");
                Console.WriteLine("0 - Exit");
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        {
                            #region conventional

                            Console.WriteLine("Conventional");
                            Console.WriteLine();

                            autofac.ContainerBuilder builder = new autofac.ContainerBuilder();

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

                            #endregion
                        }
                        break;
                    case "2":
                        {
                            #region convention-based (simple)

                            Console.WriteLine("Convention-based (simple)");
                            Console.WriteLine();

                            autofac.ContainerBuilder builder = new autofac.ContainerBuilder();

                            builder.RegisterType<SuperheroService>();
                            builder.RegisterType<Logger>().As<ILogger>();

                            Assembly assemblyToScan = typeof(SuperheroService).Assembly;
                            Assembly repositoryAssembly = typeof(AvengerRepository).Assembly;

                            builder.RegisterAssemblyTypes(assemblyToScan)
                                .Where(t => t.Name.EndsWith("Repository"))
                                .As(t => t.GetInterfaces()?.FirstOrDefault(
                                    i => i.Name == "I" + t.Name));

                            Container = builder.Build();

                            SuperheroService superheroService = Container.Resolve<SuperheroService>();
                            
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
                            #region convention-based (complex)

                            Console.WriteLine("Convention-based (complex)");
                            Console.WriteLine();

                            autofac.ContainerBuilder builder = new autofac.ContainerBuilder();

                            builder.RegisterType<SuperheroService>();

                            builder.RegisterAssemblyTypes(typeof(SuperheroService).Assembly)
                                .Where(t =>
                                {
                                    bool includeType = false;
                                    if (t.Name != "SuperheroService")
                                    {
                                        Type interfaceType = t.GetInterfaces().FirstOrDefault(
                                            i => i.Name == "I" + t.Name);
                                        if (interfaceType != null)
                                            includeType = true;
                                    }
                                    return includeType;
                                })
                                .As(t =>
                                {
                                    Type interfaceType = t.GetInterfaces().FirstOrDefault(
                                        i => i.Name == "I" + t.Name);
                                    return interfaceType;
                                });

                            Container = builder.Build();

                            SuperheroService superheroService = Container.Resolve<SuperheroService>();
                            
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
                    case "4":
                        {
                            #region attribute-based

                            Console.WriteLine("Attribute-based");
                            Console.WriteLine();

                            autofac.ContainerBuilder builder = new autofac.ContainerBuilder();

                            builder.RegisterType<SuperheroService>();
                            builder.RegisterType<Logger>().As<ILogger>();

                            builder.RegisterAssemblyTypes(typeof(SuperheroService).Assembly)
                                .Where(t =>
                                {
                                    bool includeType = false;

                                    RegisterAttribute registerAttr = t.GetCustomAttribute<RegisterAttribute>(true);
                                    if (registerAttr != null)
                                    {
                                        if (registerAttr.As != null)
                                            includeType = true;
                                    }

                                    return includeType;
                                })
                                .As(t =>
                                {
                                    Type interfaceType = null;

                                    RegisterAttribute registerAttr = t.GetCustomAttribute<RegisterAttribute>(true);
                                    if (registerAttr != null)
                                    {
                                        if (registerAttr.As != null)
                                            interfaceType = registerAttr.As;
                                    }

                                    return interfaceType;
                                });

                            Container = builder.Build();

                            SuperheroService superheroService = Container.Resolve<SuperheroService>();

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
                    case "5":
                        {
                            #region module-based

                            Console.WriteLine("Module-based");
                            Console.WriteLine();

                            autofac.ContainerBuilder builder = new autofac.ContainerBuilder();

                            builder.RegisterType<Logger>().As<ILogger>();

                            builder.RegisterModule<RepositoryRegistrationModule>();
                            builder.RegisterModule<ConcreteRegistrationModule>();
                            
                            // or
                            //builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

                            Container = builder.Build();

                            SuperheroService superheroService = Container.Resolve<SuperheroService>();

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
                    case "6":
                        {
                            #region convention-based (legacy)

                            Console.WriteLine("Configuration-based (legacy)");
                            Console.WriteLine();

                            autofac.ContainerBuilder builder = new autofac.ContainerBuilder();

                            // ensure config registration is commented out
                            builder.RegisterType<Logger>().As<ILogger>();

                            builder.RegisterModule(new ConfigurationSettingsReader("autofac"));

                            Container = builder.Build();

                            SuperheroService superheroService = Container.Resolve<SuperheroService>();

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
                    case "7":
                        {
                            #region configuration-based (new)

                            Console.WriteLine("Configuration-based (new)");
                            Console.WriteLine();

                            autofac.ContainerBuilder builder = new autofac.ContainerBuilder();

                            IConfigurationBuilder config = new ConfigurationBuilder();
                            config.AddJsonFile("autofac.json");

                            ConfigurationModule module = new ConfigurationModule(config.Build());

                            builder.RegisterModule(module);

                            Container = builder.Build();

                            SuperheroService superheroService = Container.Resolve<SuperheroService>();

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