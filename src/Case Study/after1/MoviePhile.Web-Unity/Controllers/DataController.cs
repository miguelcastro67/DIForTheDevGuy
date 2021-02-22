using Microsoft.AspNetCore.Mvc;
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
        public DataController(IMovieRepository movieRepository, IGenreRepository genreRepository, IActorRepository actorRepository)
        {
            _MovieRepository = movieRepository;
            _GenreRepository = genreRepository;
            _ActorRepository = actorRepository;
        }
        
        IMovieRepository _MovieRepository = null;
        IGenreRepository _GenreRepository = null;
        IActorRepository _ActorRepository = null;
        
        [Route("genres")]
        public IActionResult GetGenres()
        {
            var genres = _GenreRepository.Get();

            return new OkObjectResult(genres);
        }

        [HttpPost]
        [Route("movieInfo")]
        public IActionResult UpdateMovieInfo([FromForm]UpdateMovieInfoModel model)
        {
            _MovieRepository.UpdateMovieInfo(model.MovieId, model.Name, model.GenreId);

            return new OkResult();
        }

        [Route("cast/{movieId}")]
        public IActionResult GetMovieCast(int movieId)
        {
            var movie = _MovieRepository.Get(movieId);
            var cast = _ActorRepository.GetForMovie(movieId);

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
            _ActorRepository.Remove(actorId);

            return new OkResult();
        }

        [HttpPost]
        [Route("castMember")]
        public IActionResult AddCastMember([FromForm]AddCastMemberModel model)
        {
            Actor actor = new Actor()
            {
                Name = model.ActorName,
                MovieId = model.MovieId
            };

            Actor newActor = _ActorRepository.Add(actor);

            return new OkObjectResult(newActor);
        }
    }
}
