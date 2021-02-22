using Autofac;
using Lib;
using Lib.Abstractions;
using System;
using System.Linq;
using System.Windows;
using WpfClient.Core;
using WpfClient.ViewModels;

namespace WpfClient
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<ViewModelLocator>().As<IComponentLocator>();
            builder.RegisterType<MainWindowViewModel>();
            builder.RegisterType<AvengerRepository>().As<IAvengerRepository>();
            builder.RegisterType<Logger>().As<ILogger>();
            builder.RegisterType<SuperheroService>().As<ISuperheroService>();
            builder.RegisterType<AvengersViewModel>();
            builder.RegisterType<AvengerViewModel>();

            Program.Container = builder.Build();

            base.OnStartup(e);
        }
    }

    public static class Program
    {
        public static IContainer Container { get; internal set; }
    }
}