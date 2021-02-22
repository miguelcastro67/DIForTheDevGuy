using Autofac;
using Autofac.Integration.Wcf;
using Lib;
using Lib.Abstractions;
using System;
using System.Linq;
using System.ServiceModel;
using WcfHost.Services;

namespace WcfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ContainerBuilder builder = new ContainerBuilder();
            
            builder.RegisterType<AvengerRepository>().As<IAvengerRepository>();
            builder.RegisterType<Logger>().As<ILogger>();
            builder.RegisterType<SuperheroService>();

            IContainer container = builder.Build();

            ServiceHost host = new ServiceHost(typeof(SuperheroService));

            host.AddDependencyInjectionBehavior(typeof(SuperheroService), container);

            host.Open();

            Console.WriteLine("SuperheroService running. Press [Enter] to exit.");
            Console.ReadLine();
            
            host.Close();
        }
    }
}
