using Core.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MoviePhile.Data.Abstractions;
using MoviePhile.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using MoviePhile.UnityExtensions;

namespace MoviePhile.Web.Controllers
{
    [Route("movie")]
    public class MovieController : Controller
    {
        public MovieController(ILocalStrings localStrings, IMovieRepository movieRepository)
        {
            _LocalStrings = localStrings;
            _MovieRepository = movieRepository;
        }

        ILocalStrings _LocalStrings;
        IMovieRepository _MovieRepository = null;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ViewData["Title"] = "Movie Phile - " + _LocalStrings.Title;
        }

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
