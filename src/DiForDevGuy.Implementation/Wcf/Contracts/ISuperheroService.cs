using Entities;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Contracts
{
    [ServiceContract]
    public interface ISuperheroService
    {
        [OperationContract]
        Hero GetAvenger(string name);
        [OperationContract]
        IEnumerable<Hero> GetAvengers();
    }
}
