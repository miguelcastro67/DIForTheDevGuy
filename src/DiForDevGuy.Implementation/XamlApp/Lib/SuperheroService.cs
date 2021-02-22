using Lib.Abstractions;
using System;
using System.Collections.Generic;

namespace Lib
{
    public class SuperheroService : ISuperheroService
    {
        public SuperheroService(IAvengerRepository avengerRepository, ILogger logger)
        {
            _AvengerRepository = avengerRepository;
            _Logger = logger;
        }

        IAvengerRepository _AvengerRepository;
        ILogger _Logger;
        int _TimesCalled = 0;

        public IEnumerable<Hero> GetAvengers()
        {
            _TimesCalled++;

            _Logger.Log("Calling SuperheroService.GetAvengers.");

            var avengers = _AvengerRepository.FetchAll();

            _Logger.Log("SuperheroService.GetAvengers called for time #{0}.", _TimesCalled.ToString());

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
