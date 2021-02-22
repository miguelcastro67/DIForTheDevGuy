using Melvicorp.Data;
using MoviePhile.Data.Abstractions;
using MoviePhile.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Text;

namespace MoviePhile.Data
{
    public class GenreRepository : DataRepositoryBase<Genre, MoviePhileDbContext, int>, IGenreRepository
    {
        protected override DbSet<Genre> DbSet(MoviePhileDbContext entityContext)
        {
            return entityContext.GenreSet;
        }

        protected override Expression<Func<Genre, bool>> IdentifierPredicate(MoviePhileDbContext entityContext, int id)
        {
            return (e => e.GenreId == id);
        }
    }
}
