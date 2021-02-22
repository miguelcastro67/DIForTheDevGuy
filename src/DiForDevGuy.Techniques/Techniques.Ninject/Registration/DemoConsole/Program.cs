using Ext;
using Ext.Configuration;
using Lib;
using Lib.Abstractions;
using Ninject;
using Ninject.Extensions.Conventions;
using System;
using System.Linq;
using System.Reflection;

namespace DemoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Ninject - Registration Techniques");
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

                            IKernel kernel = new StandardKernel();

                            kernel.Bind<IAvengerRepository>().To<AvengerRepository>();
                            kernel.Bind<ILogger>().To<Logger>();
                            
                            SuperheroService superheroService = kernel.Get<SuperheroService>();

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

                            IKernel kernel = new StandardKernel();

                            kernel.Bind<ILogger>().To<Logger>();
                            
                            kernel.Bind(scan =>
                            {
                                scan.From(typeof(SuperheroService).Assembly)
                                          .SelectAllClasses().Where(t => t.Name.EndsWith("Repository"))
                                          .BindDefaultInterface();
                            });
                            
                            SuperheroService superheroService = kernel.Get<SuperheroService>();

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

                            IKernel kernel = new StandardKernel();

                            // comment out when using Logger2
                            //kernel.Bind<ILogger>().To<Logger>();

                            kernel.Bind(scan =>
                            {
                                scan.From(typeof(SuperheroService).Assembly)
                                    .SelectAllClasses().Where(t =>
                                    {
                                        bool includeType = false;

                                        RegisterAttribute registerAttr = t.GetCustomAttribute<RegisterAttribute>(true);
                                        if (registerAttr != null)
                                        {
                                            if (registerAttr.As != null)
                                                includeType = true;
                                        }
                                        
                                        return includeType;
                                    }).BindWith<AttributeBindingGenerator>();
                            });

                            SuperheroService superheroService = kernel.Get<SuperheroService>();

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

                            IKernel kernel = new StandardKernel();

                            kernel.Load<RepositoryRegistrationModule>();
                            kernel.Bind<ILogger>().To<Logger>();

                            SuperheroService superheroService = kernel.Get<SuperheroService>();

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

                            IKernel kernel = new StandardKernel();

                            //XDocument xDoc = XDocument.Load("ninject.xml");

                            //Dictionary<string, IModuleChildXmlElementProcessor> elementProcessors = 
                            //    kernel.Components.GetAll<IModuleChildXmlElementProcessor>()
                            //              .ToDictionary(processor => processor.XmlNodeName);

                            //IEnumerable<XElement> moduleElements = xDoc.Elements("module");
                            //List<NinjectModule> modules = new List<NinjectModule>();
                            //foreach (XElement xElement in moduleElements)
                            //{
                            //    var xMod = new XmlModule(xElement, elementProcessors);
                            //    NinjectModule ninjectModule = xMod as NinjectModule;
                            //    modules.Add(ninjectModule);
                            //}


                            //IEnumerable<INinjectModule> modules = moduleElements.Select(item =>
                            //{
                            //    var xMod = new XmlModule(item, elementProcessors);
                            //    return xMod as INinjectModule;
                            //});

                            //kernel.Load(modules);

                            //kernel.Load("ninject.xml");   

                            kernel.Load<ConfigurationSettingsReader>();

                            SuperheroService superheroService = kernel.Get<SuperheroService>();

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