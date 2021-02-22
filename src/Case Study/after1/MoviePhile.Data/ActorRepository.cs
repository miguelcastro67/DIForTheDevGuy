using Melvicorp.Data;
using MoviePhile.Data.Abstractions;
using MoviePhile.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MoviePhile.Data
{
    public class ActorRepository : DataRepositoryBase<Actor, MoviePhileDbContext, int>, IActorRepository
    {
        protected override DbSet<Actor> DbSet(MoviePhileDbContext entityContext)
        {
            return entityContext.ActorSet;
        }

        protected override Expression<Func<Actor, bool>> IdentifierPredicate(MoviePhileDbContext entityContext, int id)
        {
            return (e => e.ActorId == id);
        }

        public IEnumerable<Actor> GetForMovie(int movieId)
        {
            using (MoviePhileDbContext entityContext = new MoviePhileDbContext())
            {
                return entityContext.ActorSet.Where(item => item.MovieId == movieId).ToFullyLoaded();
            }
        }
    }
}
