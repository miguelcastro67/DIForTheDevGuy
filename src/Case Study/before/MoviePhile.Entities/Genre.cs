using Melvicorp.Data;
using System;
using System.Runtime.Serialization;

namespace MoviePhile.Entities
{
    [DataContract]
    public class Genre : IIdentifiableEntity<int>
    {
        [DataMember]
        public int GenreId { get; set; }
        [DataMember]
        public string Description { get; set; }

        public int EntityId
        {
            get { return GenreId; }
            set { GenreId = value; }
        }
    }
}
