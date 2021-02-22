using Lib.Abstractions;
using System;

namespace Lib.Handlers
{
    public class IronmanHandler : IAvengerHandler
    {
        public IronmanHandler(IAvengerRepository avengerRepository)
        {
            _AvengerRepository = avengerRepository;
        }

        IAvengerRepository _AvengerRepository;

        Hero IAvengerHandler.GetAvenger()
        {
            Hero hero = _AvengerRepository.Fetch("ironman");

            return hero;
        }
    }
}
