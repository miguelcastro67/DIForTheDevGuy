using Melvicorp.Data;
using MoviePhile.Entities;
using System;
using System.Collections.Generic;

namespace MoviePhile.Data.Abstractions
{
    public interface IActorRepository : IDataRepository<Actor, MoviePhileDbContext, int>
    {
        IEnumerable<Actor> GetForMovie(int movieId);
    }
}
