using Autofac;
using Lib.Abstractions;
using System;

namespace Lib
{
    public class SuperheroService
    {
        public SuperheroService(ILogger logger, ILifetimeScope container, string avengerName)
        {
            _Logger = logger;
            _Container = container;
            _AvengerName = avengerName;
        }

        ILogger _Logger;
        ILifetimeScope _Container;
        string _AvengerName;

        public Hero GetAvenger()
        {
            _Logger.Log("Calling SuperheroService.GetAvenger() with Avenger Name: '{0}'.", _AvengerName);

            IAvengerHandler handler = _Container.ResolveKeyed<IAvengerHandler>(_AvengerName);

            var avenger = handler.GetAvenger();

            _Logger.Log("SuperheroService.GetAvenger() called with Avenger Name: '{0}'.", _AvengerName);

            return avenger;
        }
    }
}
