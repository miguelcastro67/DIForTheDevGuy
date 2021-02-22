using Melvicorp.Data;
using MoviePhile.Data.Abstractions;
using MoviePhile.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MoviePhile.Data
{
    public class MovieRepository : DataRepositoryBase<Movie, MoviePhileDbContext, int>, IMovieRepository
    {
        protected override DbSet<Movie> DbSet(MoviePhileDbContext entityContext)
        {
            return entityContext.MovieSet;
        }

        protected override Expression<Func<Movie, bool>> IdentifierPredicate(MoviePhileDbContext entityContext, int id)
        {
            return (e => e.MovieId == id);
        }

        public Movie GetInfo(int movieId)
        {
            using (MoviePhileDbContext entityContext = new MoviePhileDbContext())
            {
                return entityContext.MovieSet
                    .Include(e => e.Genre)
                    .FirstOrDefault(e => e.MovieId == movieId);
            }
        }

        public Movie GetComplete(int movieId)
        {
            using (MoviePhileDbContext entityContext = new MoviePhileDbContext())
            {
                return entityContext.MovieSet
                    .Include(e => e.Genre)
                    .Include(e => e.Actors.Select(a => a.Movie))
                    .FirstOrDefault(e => e.MovieId == movieId);
            }
        }

        public void UpdateMovieInfo(int movieId, string movieName, int genreId)
        {
            using (MoviePhileDbContext entityContext = new MoviePhileDbContext())
            {
                Movie movie = entityContext.MovieSet.FirstOrDefault(item => item.MovieId == movieId);
                if (movie != null)
                {
                    movie.Name = movieName;
                    movie.GenreId = genreId;

                    entityContext.SaveChanges();
                }
            }
        }
    }
}
