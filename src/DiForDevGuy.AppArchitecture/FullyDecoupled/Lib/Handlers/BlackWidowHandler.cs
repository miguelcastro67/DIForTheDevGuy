using Lib.Abstractions;
using System;

namespace Lib.Handlers
{
    public class BlackWidowHandler : IAvengerHandler
    {
        public BlackWidowHandler(IAvengerRepository avengerRepository)
        {
            _AvengerRepository = avengerRepository;
        }

        IAvengerRepository _AvengerRepository;

        Hero IAvengerHandler.GetAvenger()
        {
            Hero hero = _AvengerRepository.Fetch("blackwidow");

            return hero;
        }
    }
}
