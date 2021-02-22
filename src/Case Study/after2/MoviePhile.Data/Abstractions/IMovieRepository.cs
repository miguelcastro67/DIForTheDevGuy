using Melvicorp.Data;
using MoviePhile.Entities;
using System;

namespace MoviePhile.Data.Abstractions
{
    public interface IMovieRepository : IDataRepository<Movie, MoviePhileDbContext, int>
    {
        void UpdateMovieInfo(int movieId, string movieName, int genreId);
        Movie GetInfo(int movieId);
        Movie GetComplete(int movieId);
    }
}
