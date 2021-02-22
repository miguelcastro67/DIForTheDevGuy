using Lib.Abstractions;
using System;
using System.Collections.Generic;

namespace Lib
{
    public class SuperheroService_Old
    {
        public SuperheroService_Old(IAvengerRepository avengerRepository, ILogger logger)
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
            var avenger = _AvengerRepository.Fetch(name);

            return avenger;
        }
    }
}
