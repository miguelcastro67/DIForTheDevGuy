using Lib.Abstractions;
using System;
using System.Collections.Generic;

namespace Lib
{
    public class SuperheroService3
    {
        public SuperheroService3(ILogger logger, IAvengerHandler avengerHandler)
        {
            _Logger = logger;
            _AvengerHandler = avengerHandler;
        }
        
        ILogger _Logger;
        IAvengerHandler _AvengerHandler;

        public Hero GetAvenger()
        {
            _Logger.Log("Calling SuperheroService.GetAvenger() with Avenger Handler: '{0}'.", _AvengerHandler.GetType().Name);

            var avenger = _AvengerHandler.GetAvenger();

            _Logger.Log("SuperheroService.GetAvenger() called with Avenger Handler: '{0}'.", _AvengerHandler.GetType().Name);

            return avenger;
        }
    }
}
