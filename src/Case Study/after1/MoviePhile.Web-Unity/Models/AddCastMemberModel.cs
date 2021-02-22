using System;
using System.Linq;

namespace MoviePhile.Web.Model
{
    public class AddCastMemberModel
    {
        public int MovieId { get; set; }
        public string ActorName { get; set; }
    }
}
