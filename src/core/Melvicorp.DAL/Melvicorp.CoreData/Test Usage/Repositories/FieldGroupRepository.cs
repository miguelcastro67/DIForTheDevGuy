using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Melvicorp.CoreData.Test
{
    public class FieldGroupRepository : DataRepositoryBase<FieldGroup, MyDbContext, Int32>, IFieldGroupRepository
    {
        protected override DbSet<FieldGroup> DbSet(MyDbContext entityContext)
        {
            return entityContext.FieldGroupSet;
        }
        
        protected override Expression<Func<FieldGroup, bool>> IdentifierPredicate(MyDbContext entityContext, Int32 id)
        {
            return (e => e.FieldGroupId == id);
        }

        // other DAL methods (defined in interface)

        public FieldGroup GetFirstWithGroupName(string groupName)
        {
            using (MyDbContext entityContext = new MyDbContext())
            {
                return entityContext.FieldGroupSet.FirstOrDefault(item => item.GroupName == groupName);
            }
        }

        public IEnumerable<FieldGroup> GetAllByGroup(string groupName)
        {
            using (MyDbContext entityContext = new MyDbContext())
            {
                return entityContext.FieldGroupSet.Where(item => item.GroupName == groupName).ToFullyLoaded();
            }
        }
    }
}
