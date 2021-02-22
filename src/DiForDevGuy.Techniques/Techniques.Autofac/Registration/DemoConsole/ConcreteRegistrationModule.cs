using Autofac;
using Lib;
using Lib.Abstractions;
using System;
using System.Linq;

namespace DemoConsole
{
    public class ConcreteRegistrationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SuperheroService>();
        }
    }
}
