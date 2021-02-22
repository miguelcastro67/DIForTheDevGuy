using Lib.Abstractions;
using System;
using System.Collections.Generic;

namespace Lib
{
    public class SuperheroService
    {
        public SuperheroService(IRepository repository, ILogger logger)
        {
            _Repository = repository;
            _Logger = logger;
        }

        IRepository _Repository;
        ILogger _Logger;

        public IEnumerable<Hero> GetAvengers()
        {
            _Logger.Log("Calling SuperheroService.GetAvengers.");

            var avengers = _Repository.FetchAll();

            _Logger.Log("SuperheroService.GetAvengers called.");

            return avengers;
        }

        public Hero GetAvenger(string name)
        {
            _Logger.Log("Calling SuperheroService.GetAvenger('{0}').", name);

            var avenger = _Repository.Fetch(name);

            _Logger.Log("SuperheroService.GetAvenger('{0}') called.", name);

            return avenger;
        }
    }
}
