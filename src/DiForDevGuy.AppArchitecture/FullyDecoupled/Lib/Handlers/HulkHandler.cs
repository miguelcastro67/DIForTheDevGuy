using Lib.Abstractions;
using System;

namespace Lib.Handlers
{
    public class HulkHandler : IAvengerHandler
    {
        public HulkHandler(IAvengerRepository avengerRepository)
        {
            _AvengerRepository = avengerRepository;
        }

        IAvengerRepository _AvengerRepository;

        Hero IAvengerHandler.GetAvenger()
        {
            Hero hero = _AvengerRepository.Fetch("hulk");

            return hero;
        }
    }
}
