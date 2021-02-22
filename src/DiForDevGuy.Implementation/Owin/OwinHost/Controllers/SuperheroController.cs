using Lib;
using Lib.Abstractions;
using OwinHost.Core;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiHost.Controllers
{
    [RoutePrefix("api/superhero")]
    public class SuperheroController : ApiController
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
        public HttpResponseMessage GetAvengers(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;

            _Logger.Log("Calling SuperheroService.GetAvengers.");

            var avengers = _AvengerRepository.FetchAll();
            if (avengers != null)
                response = request.CreateResponse<Hero[]>(HttpStatusCode.OK, avengers.ToArray());
            else
                response = request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Cannot get list of avenger list.");

            _Logger.Log("SuperheroService.GetAvengers called.");

            return response;
        }

        [MyCustomFilter]
        [HttpGet]
        [Route("avenger/{name}")]
        public HttpResponseMessage GetAvenger(HttpRequestMessage request, string name)
        {
            HttpResponseMessage response = null;

            _Logger.Log("Calling SuperheroService.GetAvenger('{0}').", name);

            var avenger = _AvengerRepository.Fetch(name);
            if (avenger != null)
                response = request.CreateResponse<Hero>(HttpStatusCode.OK, avenger);
            else
                response = request.CreateErrorResponse(HttpStatusCode.NoContent, name);
            
            _Logger.Log("SuperheroService.GetAvenger('{0}') called.", name);

            return response;
        }
    }
}