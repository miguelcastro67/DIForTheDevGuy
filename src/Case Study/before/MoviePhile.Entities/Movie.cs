using Melvicorp.Data;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MoviePhile.Entities
{
    [DataContract]
    public class Movie : IIdentifiableEntity<int>
    {
        [DataMember]
        public int MovieId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int GenreId { get; set; }
        [DataMember]
       public ICollection<Actor> Actors { get; set; }
        [DataMember]
        public Genre Genre { get; set; }

        public int EntityId
        {
            get { return MovieId; }
            set { MovieId = value; }
        }
    }
}
