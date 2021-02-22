using System;
using System.Collections.Generic;

namespace Lib
{
    public class SuperheroService
    {
        public IEnumerable<Hero> GetAvengers()
        {
            Logger logger = new Logger();
            logger.Log("Calling SuperheroService.GetAvengers.");

            AvengerRepository avengerRepository = new AvengerRepository();
            var avengers = avengerRepository.FetchAll();

            logger.Log("SuperheroService.GetAvengers called.");

            return avengers;
        }

        public Hero GetAvenger(string name)
        {
            Logger logger = new Logger();
            logger.Log("Calling SuperheroService.GetAvenger('{0}').", name);

            AvengerRepository avengerRepository = new AvengerRepository();
            var avenger = avengerRepository.Fetch(name);

            logger.Log("SuperheroService.GetAvenger('{0}') called.", name);

            return avenger;
        }
    }
}
