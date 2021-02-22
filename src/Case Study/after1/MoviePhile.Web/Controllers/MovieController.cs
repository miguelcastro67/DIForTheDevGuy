using Microsoft.AspNetCore.Mvc;
using MoviePhile.Data;
using MoviePhile.Data.Abstractions;
using MoviePhile.Entities;
using MoviePhile.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviePhile.Web.Controllers
{
    [Route("movie")]
    public class MovieController : Controller
    {
        [HttpGet("list")]
        public IActionResult ListMovies()
        {
            IMovieRepository movieRepository = new MovieRepository();
            IEnumerable<Movie> movies = movieRepository.Get();

            return View(movies);
        }
        
        [HttpGet("info/{movieId}")]
        public IActionResult MovieInfo(int movieId)
        {
            IMovieRepository movieRepository = new MovieRepository();

            var movie = movieRepository.GetInfo(movieId);

            return View(movie);
        }
        
        [HttpGet("cast/{movieId}")]
        public IActionResult MovieCast(int movieId)
        {
            //IMovieRepository movieRepository = new MovieRepository();
            //IActorRepository actorRepository = new ActorRepository();

            //var movie = movieRepository.Get(movieId);
            //var cast = actorRepository.GetForMovie(movieId);

            //MovieActorsModel model = new MovieActorsModel()
            //{
            //    Movie = movie,
            //    Cast = cast
            //};

            return View(movieId);
        }
    }
}
