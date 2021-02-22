using Lib.Abstractions;
using Ninject;
using System;
using System.Collections.Generic;

namespace Lib
{
    public class SuperheroService
    {
        // note that to use property injection in Ninject, you MUST reference Ninject

        [Inject]
        public IAvengerRepository _AvengerRepository { private get; set; }
        [Inject]
        public ILogger _Logger { private get; set; }

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
