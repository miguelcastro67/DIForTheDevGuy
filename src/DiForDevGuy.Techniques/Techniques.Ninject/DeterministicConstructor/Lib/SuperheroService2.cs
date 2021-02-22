using Lib.Abstractions;
using Ninject;
using System;
using System.Collections.Generic;

namespace Lib
{
    public class SuperheroService2
    {
        //[Inject]
        public SuperheroService2(IAvengerRepository avengerRepository, ILogger logger)
        {
            _AvengerRepository = avengerRepository;
            _Logger = logger;
        }

        public SuperheroService2(IAvengerRepository avengerRepository, ILogger logger, string value1)
        {
        }

        IAvengerRepository _AvengerRepository;
        ILogger _Logger;

        public IEnumerable<Hero> GetAvengers()
        {

            _Logger.Log("Calling SuperheroService.GetAvengers.");

            var avengers = _AvengerRepository.FetchAll();

            _Logger.Log("SuperheroService.GetAvengers called.");

            return avengers;
        }

        public Hero GetAvenger(string name)
        {
            _Logger.Log("Calling SuperheroService.GetAvenger('{0}').", name);

            var avenger = _AvengerRepository.Fetch(name);

            _Logger.Log("SuperheroService.GetAvenger('{0}') called.", name);

            return avenger;
        }
    }
}
