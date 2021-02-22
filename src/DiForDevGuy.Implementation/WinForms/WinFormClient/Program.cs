using Autofac;
using Lib;
using Lib.Abstractions;
using System;
using System.Linq;
using System.Windows.Forms;
using WinFormClient.Core;

namespace WinFormClient
{
    static class Program
    {
        public static IContainer Container { get; private set; }

        [STAThread]
        static void Main()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<FormLocator>().As<IComponentLocator>();
            builder.RegisterType<AvengerRepository>().As<IAvengerRepository>();
            builder.RegisterType<Logger>().As<ILogger>();
            builder.RegisterType<SuperheroService>().As<ISuperheroService>();
            builder.RegisterType<Form1>();
            builder.RegisterType<AvengersForm>();
            builder.RegisterType<AvengerForm>();

            Container = builder.Build();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Container.Resolve<Form1>());
        }
    }
}
