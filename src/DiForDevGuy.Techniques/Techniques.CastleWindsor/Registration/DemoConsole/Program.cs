using Castle.MicroKernel.Registration;
using Castle.Windsor.Installer;
using Ext;
using Lib;
using Lib.Abstractions;
using System;
using System.Linq;
using System.Reflection;
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
                Console.WriteLine("Castle Windsor - Registration Techniques");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("1 - Get all Avengers - conventional");
                Console.WriteLine("2 - Get all Avengers - convention-based");
                Console.WriteLine("3 - Get all Avengers - attribute-based");
                Console.WriteLine("4 - Get all Avengers - module-based");
                Console.WriteLine("5 - Get all Avengers - configuration-based");
                Console.WriteLine("0 - Exit");
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        {
                            Console.WriteLine("Conventional");
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
                        }
                        break;
                    case "2":
                        {
                            Console.WriteLine("Convention-based");
                            Console.WriteLine();

                            windsor.WindsorContainer container = new windsor.WindsorContainer();

                            container.Register(Component.For<ILogger>().ImplementedBy<Logger>());
                            container.Register(Component.For<SuperheroService>());

                            container.Register(Classes.FromAssembly(typeof(SuperheroService).Assembly)
                                .Where(t => t.Name.EndsWith("Repository"))
                                .WithService.Select((t, b) => new[] { t.GetInterfaces()?.FirstOrDefault(
                                    i => i.Name == "I" + t.Name) } ));

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
                    case "3":
                        {
                            Console.WriteLine("Attribute-based");
                            Console.WriteLine();

                            windsor.WindsorContainer container = new windsor.WindsorContainer();

                            // comment next line out to use Logger2
                            //container.Register(Component.For<ILogger>().ImplementedBy<Logger>());

                            container.Register(Component.For<SuperheroService>());

                            container.Register(Classes.FromAssembly(typeof(SuperheroService).Assembly)
                                .Where(t =>
                                {
                                    bool includeType = false;

                                    RegisterAttribute registerAttr = t.GetCustomAttribute<RegisterAttribute>(true);
                                    if (registerAttr != null)
                                    {
                                        if (registerAttr.For != null)
                                            includeType = true;
                                    }
                                    
                                    return includeType;
                                })
                                .WithService.Select((t, b) =>
                                {
                                    Type interfaceType = null;

                                    RegisterAttribute registerAttr = t.GetCustomAttribute<RegisterAttribute>(true);
                                    if (registerAttr != null)
                                    {
                                        if (registerAttr.For != null)
                                            interfaceType = registerAttr.For;
                                    }

                                    return new[] { interfaceType };
                                }));

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
                    case "4":
                        {
                            Console.WriteLine("Module-based");
                            Console.WriteLine();

                            windsor.WindsorContainer container = new windsor.WindsorContainer();

                            container.Register(Component.For<ILogger>().ImplementedBy<Logger>());
                            container.Register(Component.For<SuperheroService>());

                            container.Install(new RepositoryRegistrationModule());

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
                    case "5":
                        {
                            Console.WriteLine("Configuration-based");
                            Console.WriteLine();

                            windsor.WindsorContainer container = new windsor.WindsorContainer();
                            
                            ConfigurationInstaller config = Configuration.FromXmlFile("castle.xml");

                            container.Install(config);

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
                    case "0":
                        exit = true;
                        break;
                }
                
                Console.WriteLine();
            }
        }
    }
}