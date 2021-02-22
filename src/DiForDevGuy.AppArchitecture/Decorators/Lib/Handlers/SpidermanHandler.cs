using Lib.Abstractions;
using System;

namespace Lib.Handlers
{
    public class SpidermanHandler : IAvengerHandler
    {
        public SpidermanHandler(IAvengerRepository avengerRepository)
        {
            _AvengerRepository = avengerRepository;
        }

        IAvengerRepository _AvengerRepository;

        Hero IAvengerHandler.GetAvenger()
        {
            Hero hero = _AvengerRepository.Fetch("spiderman");

            return hero;
            ;
        }
    }
}
