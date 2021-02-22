using System;
using System.Linq;

namespace Melvicorp.CoreData.Test
{
    public class FieldGroup : IIdentifiableEntity<int>
    {
        public int FieldGroupId { get; set; }
        public string GroupName { get; set; }

        public int EntityId
        {
            get { return FieldGroupId; }
            set { FieldGroupId = value; }
        }
    }
}
