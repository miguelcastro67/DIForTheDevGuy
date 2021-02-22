using Core.Common;
using Lib.Abstractions;
using System;
using System.Collections.Generic;

namespace Lib
{
    public class SuperheroService
    {
        public SuperheroService(IAvengerRepository avengerRepository, IConfigurationFactory configurationFactory, IComponentLocator componentLocator)
        {
            _AvengerRepository = avengerRepository;
            _Loggers = configurationFactory.GetLoggers();
            _ComponentLocator = componentLocator;
        }
        
        IAvengerRepository _AvengerRepository;
        IEnumerable<ILogger> _Loggers;
        IComponentLocator _ComponentLocator;

        public IEnumerable<Hero> GetAvengers()
        {
            Log("Calling SuperheroService.GetAvengers.");
            
            var avengers = _AvengerRepository.FetchAll();

            Log("SuperheroService.GetAvengers called.");

            return avengers;
        }
        
        public Hero GetAvenger(string name)
        {
            IAvengerHandler avengerHandler = _ComponentLocator.ResolveComponent<IAvengerHandler>(name.Replace(" ", ""));

            Log("Calling SuperheroService.GetAvenger() with Avenger Handler: '{0}'.", avengerHandler.GetType().Name);

            var avenger = avengerHandler.GetAvenger();

            Log("SuperheroService.GetAvenger() called with Avenger Handler: '{0}'.", avengerHandler.GetType().Name);

            return avenger;
        }

        void Log(string message, params string[] args)
        {
            foreach (ILogger logger in _Loggers)
                logger.Log(message, args);
        }
    }
}
