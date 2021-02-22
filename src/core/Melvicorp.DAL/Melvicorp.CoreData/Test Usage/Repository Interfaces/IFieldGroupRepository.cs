using System;
using System.Collections.Generic;
using System.Linq;

namespace Melvicorp.CoreData.Test
{

    // NOTE, your interfaces now needs to sit in the same project as the DbContext (or the other way around)

    public interface IFieldGroupRepository : IDataRepository<FieldGroup, MyDbContext, Int32>
    {
        FieldGroup GetFirstWithGroupName(string groupName);
        IEnumerable<FieldGroup> GetAllByGroup(string groupName);
        
        // additionally defined DAL methods
    }
}
