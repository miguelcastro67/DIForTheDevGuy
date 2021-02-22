using Autofac;
using Contracts;
using Entities;
using System;
using System.Collections.Generic;

namespace WcfClient
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("WCF Client");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("1 - Get all Avengers");
                Console.WriteLine("2 - Get single Avenger");
                Console.WriteLine("3 - Get all Avengers - client DI (incorrect)");
                Console.WriteLine("4 - Get all Avengers - client DI (correct)");
                Console.WriteLine("0 - Exit");
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        {
                            using (SuperheroClient client = new SuperheroClient())
                            {
                                IEnumerable<Hero> avengers = client.GetAvengers();
                                Console.WriteLine();
                                foreach (var avenger in avengers)
                                {
                                    Console.WriteLine("{0}, who is really {1}, and has {2}.",
                                        avenger.SuperheroName, avenger.RealName, avenger.Power);
                                }
                            }
                        }
                        break;
                    case "2":
                        {
                            Console.Write("Enter superhero name: ");
                            string superheroName = Console.ReadLine();
                            if (superheroName != "")
                            {
                                using (SuperheroClient client = new SuperheroClient())
                                {
                                    Hero avenger = client.GetAvenger(superheroName);
                                    if (avenger != null)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("{0}, who is really {1}, and has {2}.",
                                            avenger.SuperheroName, avenger.RealName, avenger.Power);
                                    }
                                }
                            }                            
                        }
                        break;
                    case "3":
                        {
                            ContainerBuilder builder = new ContainerBuilder();

                            builder.RegisterType<SuperheroClient>().As<ISuperheroService>();
                            
                            IContainer container = builder.Build();

                            ISuperheroService client = container.Resolve<ISuperheroService>();

                            IEnumerable<Hero> avengers = client.GetAvengers();
                            
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
                            ContainerBuilder builder = new ContainerBuilder();

                            builder.RegisterType<SuperheroClient>().As<ISuperheroService>().InstancePerLifetimeScope();

                            IContainer container = builder.Build();

                            using (ILifetimeScope lifetimeScope = container.BeginLifetimeScope())
                            {
                                ISuperheroService client = lifetimeScope.Resolve<ISuperheroService>();

                                IEnumerable<Hero> avengers = client.GetAvengers();
                                
                                Console.WriteLine();
                                foreach (var avenger in avengers)
                                {
                                    Console.WriteLine("{0}, who is really {1}, and has {2}.",
                                        avenger.SuperheroName, avenger.RealName, avenger.Power);
                                }
                            }
                        }
                        break;
                    case "0":
                        exit = true;
                        break;
                }
            }
        }
    }
}
