using Lib.Abstractions;
using System;
using System.Collections.Generic;

namespace Lib
{
    public class SuperheroService
    {
        public SuperheroService(ILogger logger, IEnumerable<IAvengerHandler> avengerHandlers)
        {
            _Logger = logger;
            _AvengerHandlers = avengerHandlers;
        }

        ILogger _Logger;
        IEnumerable<IAvengerHandler> _AvengerHandlers;

        public IEnumerable<Hero> GetAvengers()
        {
            _Logger.Log("Calling SuperheroService.GetAvengers.");

            List<Hero> avengers = new List<Hero>();

            foreach (IAvengerHandler avengerHandler in _AvengerHandlers)
            {
                var avenger = avengerHandler.GetAvenger();
                avengers.Add(avenger);
            }

            _Logger.Log("SuperheroService.GetAvengers called.");

            return avengers;
        }

        public Hero GetAvenger(IAvengerHandler avengerHandler)
        {
            _Logger.Log("Calling SuperheroService.GetAvenger() with Avenger Handler: '{0}'.", avengerHandler.GetType().Name);

            var avenger = avengerHandler.GetAvenger();

            _Logger.Log("SuperheroService.GetAvenger() called with Avenger Handler: '{0}'.", avengerHandler.GetType().Name);

            return avenger;
        }
    }
}
