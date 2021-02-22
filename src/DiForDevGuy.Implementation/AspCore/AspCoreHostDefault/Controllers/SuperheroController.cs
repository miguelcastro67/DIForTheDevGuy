using AspCoreHostDefault.Core;
using Lib.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.Http;

namespace AspCoreHost.Controllers
{
    [Route("api/superhero")]
    public class SuperheroController : Controller
    {
        public SuperheroController(IAvengerRepository avengerRepository, ILogger logger)
        {
            _AvengerRepository = avengerRepository;
            _Logger = logger;
        }

        IAvengerRepository _AvengerRepository;
        ILogger _Logger;

        [HttpGet]
        [Route("avengers")]
        public IActionResult GetAvengers()
        {
            IActionResult response = null;

            _Logger.Log("Calling SuperheroService.GetAvengers.");

            var avengers = _AvengerRepository.FetchAll();
            if (avengers != null)
                response = new OkObjectResult(avengers.ToArray());

            _Logger.Log("SuperheroService.GetAvengers called.");

            return response;
        }

        [MyCustomFilter("this is value1", "this is value2")]
        [HttpGet]
        [Route("avenger/{name}")]
        public IActionResult GetAvenger(HttpRequestMessage request, string name)
        {
            IActionResult response = null;

            _Logger.Log("Calling SuperheroService.GetAvenger('{0}').", name);

            var avenger = _AvengerRepository.Fetch(name);
            if (avenger != null)
                response = new OkObjectResult(avenger);
            else
                response = new NotFoundResult();
            
            _Logger.Log("SuperheroService.GetAvenger('{0}') called.", name);

            return response;
        }
    }
}