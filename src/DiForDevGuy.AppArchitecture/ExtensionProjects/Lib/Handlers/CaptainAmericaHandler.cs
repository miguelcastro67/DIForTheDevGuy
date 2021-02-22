using Lib.Abstractions;
using System;

namespace Lib.Handlers
{
    public class CaptainAmericaHandler : IAvengerHandler
    {
        public CaptainAmericaHandler(IAvengerRepository avengerRepository)
        {
            _AvengerRepository = avengerRepository;
        }

        IAvengerRepository _AvengerRepository;

        Hero IAvengerHandler.GetAvenger()
        {
            Hero hero = _AvengerRepository.Fetch("captainamerica");

            return hero;
        }
    }
}
