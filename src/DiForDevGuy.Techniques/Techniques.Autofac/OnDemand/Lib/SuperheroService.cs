using Lib.Abstractions;
using System;
using System.Collections.Generic;

namespace Lib
{
    public class SuperheroService
    {
        public SuperheroService(IComponentLocator componentLocator)
        {
            _ComponentLocator = componentLocator;
        }

        IComponentLocator _ComponentLocator;

        public IEnumerable<Hero> GetAvengers()
        {
            IAvengerRepository avengerRepository = _ComponentLocator.ResolveComponent<IAvengerRepository>();
            ILogger logger = _ComponentLocator.ResolveComponent<ILogger>();

            logger.Log("Calling SuperheroService.GetAvengers.");

            var avengers = avengerRepository.FetchAll();
            
            logger.Log("SuperheroService.GetAvengers called.");

            return avengers;
        }

        public Hero GetAvenger(string name)
        {
            IAvengerRepository avengerRepository = _ComponentLocator.ResolveComponent<IAvengerRepository>();

            var avenger = avengerRepository.Fetch(name);

            return avenger;
        }
    }
}
