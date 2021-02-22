using Contracts;
using Entities;
using Lib.Abstractions;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace WcfHost.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class SuperheroService : ISuperheroService
    {
        //public SuperheroService()
        //{
        //}
        
        public SuperheroService(IAvengerRepository avengerRepository, ILogger logger)
        {
            _AvengerRepository = avengerRepository;
            _Logger = logger;
        }

        IAvengerRepository _AvengerRepository;
        ILogger _Logger;
        
        public IEnumerable<Hero> GetAvengers()
        {
            _Logger.Log("Calling SuperheroService.GetAvengers.");

            var avengers = _AvengerRepository.FetchAll();
            
            _Logger.Log("SuperheroService.GetAvengers called..");

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
