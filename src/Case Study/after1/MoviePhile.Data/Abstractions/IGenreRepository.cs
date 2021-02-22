using Melvicorp.Data;
using MoviePhile.Entities;
using System;

namespace MoviePhile.Data.Abstractions
{
    public interface IGenreRepository : IDataRepository<Genre, MoviePhileDbContext, int>
    {
    }
}
