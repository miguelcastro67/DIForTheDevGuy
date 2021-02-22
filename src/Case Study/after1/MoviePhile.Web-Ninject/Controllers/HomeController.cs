using Microsoft.AspNetCore.Mvc;
using MoviePhile.Web.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace MoviePhile.Web.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        [HttpGet("Index")]
        [Route("/")]
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
