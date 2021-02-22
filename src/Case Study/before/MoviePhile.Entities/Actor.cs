using Melvicorp.Data;
using System;
using System.Runtime.Serialization;

namespace MoviePhile.Entities
{
    [DataContract]
    public class Actor : IIdentifiableEntity<int>
    {
        [DataMember]
        public int ActorId { get; set; }
        [DataMember]
        public int MovieId { get; set; }
        [DataMember]
        public string Name { get; set; }

        public Movie Movie { get; set; }

        public int EntityId
        {
            get { return ActorId; }
            set { ActorId = value; }
        }
    }
}
