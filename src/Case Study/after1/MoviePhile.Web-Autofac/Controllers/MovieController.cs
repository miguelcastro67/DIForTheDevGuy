using Microsoft.AspNetCore.Mvc;
using MoviePhile.Data.Abstractions;
using MoviePhile.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviePhile.Web.Controllers
{
    [Route("movie")]
    public class MovieController : Controller
    {
        public MovieController(IMovieRepository movieRepository)
        {
            _MovieRepository = movieRepository;
        }

        IMovieRepository _MovieRepository = null;

        [HttpGet("list")]
        public IActionResult ListMovies()
        {
            IEnumerable<Movie> movies = _MovieRepository.Get();

            return View(movies);
        }
        
        [HttpGet("info/{movieId}")]
        public IActionResult MovieInfo(int movieId)
        {
            var movie = _MovieRepository.GetInfo(movieId);

            return View(movie);
        }
        
        [HttpGet("cast/{movieId}")]
        public IActionResult MovieCast(int movieId)
        {
            return View(movieId);
        }
    }
}
