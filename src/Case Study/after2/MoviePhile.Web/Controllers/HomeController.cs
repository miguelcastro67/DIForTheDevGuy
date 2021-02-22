using Core.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MoviePhile.UnityExtensions;
using MoviePhile.Web.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace MoviePhile.Web.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        public HomeController(ILocalStrings localStrings)
        {
            _LocalStrings = localStrings;
        }

        ILocalStrings _LocalStrings;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ViewData["Title"] = "Movie Phile - " + _LocalStrings.Title;
        }

        [HttpGet("Index")]
        [Route("/")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
