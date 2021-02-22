using MoviePhile.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviePhile.Web.Models
{
    public class MovieActorsModel
    {
        public Movie Movie { get; set; }
        public IEnumerable<Actor> Cast { get; set; }
    }
}
