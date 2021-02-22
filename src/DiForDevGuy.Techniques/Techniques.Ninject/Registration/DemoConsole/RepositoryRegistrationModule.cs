using Lib;
using Lib.Abstractions;
using Ninject.Extensions.Conventions;
using Ninject.Modules;
using System;
using System.Linq;

namespace DemoConsole
{
    public class RepositoryRegistrationModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind(convention =>
            {
                convention.From(typeof(SuperheroService).Assembly)
                          .SelectAllClasses().Where(t => t.Name.EndsWith("Repository"))
                          .BindSingleInterface();
            });
        }
    }
}
