using System;
using System.Collections.Generic;
using System.Linq;

namespace Melvicorp.CoreData
{
    public interface IIdentifiableEntity<T>
    {
        T EntityId { get; set; }
    }
}
