using Lib.Abstractions;
using System;

namespace Lib.Handlers
{
    public class ThorHandler : IAvengerHandler
    {
        public ThorHandler(IAvengerRepository avengerRepository)
        {
            _AvengerRepository = avengerRepository;
        }

        IAvengerRepository _AvengerRepository;

        Hero IAvengerHandler.GetAvenger()
        {
            Hero hero = _AvengerRepository.Fetch("thor");

            return hero;
        }
    }
}
