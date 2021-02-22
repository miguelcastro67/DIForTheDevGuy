using Microsoft.AspNetCore.Mvc;
using MoviePhile.Data;
using MoviePhile.Data.Abstractions;
using MoviePhile.Entities;
using MoviePhile.Web.Model;
using MoviePhile.Web.Models;
using System;
using System.Linq;

namespace MoviePhile.Web.Controllers
{
    [Route("api")]
    public class DataController : Controller
    {
        [Route("genres")]
        public IActionResult GetGenres()
        {
            IGenreRepository genreRepository = new GenreRepository();

            var genres = genreRepository.Get();

            return new OkObjectResult(genres);
        }

        [HttpPost]
        [Route("movieInfo")]
        public IActionResult UpdateMovieInfo([FromForm]UpdateMovieInfoModel model)
        {
            IMovieRepository movieRepository = new MovieRepository();

            movieRepository.UpdateMovieInfo(model.MovieId, model.Name, model.GenreId);

            return new OkResult();
        }

        [Route("cast/{movieId}")]
        public IActionResult GetMovieCast(int movieId)
        {
            IMovieRepository movieRepository = new MovieRepository();
            IActorRepository actorRepository = new ActorRepository();

            var movie = movieRepository.Get(movieId);
            var cast = actorRepository.GetForMovie(movieId);

            MovieActorsModel model = new MovieActorsModel()
            {
                Movie = movie,
                Cast = cast
            };

            return new OkObjectResult(model);
        }

        [HttpPost]
        [Route("castMember/{actorId}/delete")]
        public IActionResult DeleteCastMember(int actorId)
        {
            IActorRepository actorRepository = new ActorRepository();

            actorRepository.Remove(actorId);

            return new OkResult();
        }

        [HttpPost]
        [Route("castMember")]
        public IActionResult AddCastMember([FromForm]AddCastMemberModel model)
        {
            IActorRepository actorRepository = new ActorRepository();

            Actor actor = new Actor()
            {
                Name = model.ActorName,
                MovieId = model.MovieId
            };

            Actor newActor = actorRepository.Add(actor);

            return new OkObjectResult(newActor);
        }
    }
}
