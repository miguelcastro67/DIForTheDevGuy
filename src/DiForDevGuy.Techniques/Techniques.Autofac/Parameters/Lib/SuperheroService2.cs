using Autofac;
using Lib.Abstractions;
using System;

namespace Lib
{
    public class SuperheroService2
    {
        public SuperheroService2(ILogger logger, ILifetimeScope container, string avengerName)
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
            // In reality, I wouldn't be using this technique like this. I'd just accept the string param here
            // and then look it up.

            _Logger.Log("Calling SuperheroService.GetAvenger() with Avenger Name: '{0}'.", _AvengerName);

            IAvengerHandler handler = _Container.ResolveKeyed<IAvengerHandler>(_AvengerName);

            var avenger = handler.GetAvenger();

            _Logger.Log("SuperheroService.GetAvenger() called with Avenger Name: '{0}'.", _AvengerName);

            return avenger;
        }
    }
}
