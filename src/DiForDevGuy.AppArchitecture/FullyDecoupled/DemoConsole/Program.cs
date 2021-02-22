using Core.Common;
using Lib;
using System;
using System.Configuration;
using System.Linq;

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
                Console.WriteLine("1 - Get all Avengers - static-class decorator");
                Console.WriteLine("2 - Get single Avenger - keyed-resolve wrapper");
                Console.WriteLine("0 - Exit");
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        {
                            #region static-class decorator

                            Console.WriteLine("Static-Class Wrapper");
                            Console.WriteLine();
                            
                            Type containerManagerType = Type.GetType(ConfigurationManager.AppSettings["containerManager"]);
                            if (containerManagerType != null)
                            {
                                IContainerManager containerManager = Activator.CreateInstance(containerManagerType) as IContainerManager;
                                if (containerManager != null)
                                {
                                    containerManager.BuildContainer();

                                    IComponentLocator componentLocator = containerManager.GetLocator();
                                    SuperheroService superheroService = componentLocator.ResolveComponent<SuperheroService>();

                                    var avengers = superheroService.GetAvengers();
                                    Console.WriteLine();
                                    foreach (var avenger in avengers)
                                    {
                                        Console.WriteLine("{0}, who is really {1}, and has {2}.",
                                            avenger.SuperheroName, avenger.RealName, avenger.Power);
                                    }
                                }
                            }

                            #endregion
                        }
                        break;
                    case "2":
                        {
                            #region keyed-resolve wrapper

                            Console.WriteLine("Keyed-Resolve Wrapper");
                            Console.WriteLine();

                            Type containerManagerType = Type.GetType(ConfigurationManager.AppSettings["containerManager"]);
                            if (containerManagerType != null)
                            {
                                IContainerManager containerManager = Activator.CreateInstance(containerManagerType) as IContainerManager;
                                if (containerManager != null)
                                {
                                    containerManager.BuildContainer();

                                    Console.Write("Enter avenger name: ");

                                    string name = Console.ReadLine();
                                    if (name != "")
                                    {
                                        IComponentLocator componentLocator = containerManager.GetLocator();
                                        SuperheroService superheroService = componentLocator.ResolveComponent<SuperheroService>();

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
