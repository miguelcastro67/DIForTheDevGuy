using Lib;
using Lib.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcClient.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ISuperheroService superheroService)
        {
            _SuperheroService = superheroService;
        }

        ISuperheroService _SuperheroService;

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Avengers()
        {
            IEnumerable<Hero> heros = _SuperheroService.GetAvengers();
            return View(heros);
        }
        
        //[MyCustomFilter]
        public ActionResult Avenger(string name)
        {
            Hero hero = _SuperheroService.GetAvenger(name);
            return View(hero);
        }
    }
}