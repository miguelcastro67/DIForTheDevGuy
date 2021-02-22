using Lib.Abstractions;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MvcClient.Core
{
    public class MyCustomFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // just hypothetical example where this filter may need a dependency like the superhero service

            var superheroService = DependencyResolver.Current.GetService<ISuperheroService>();
        }
    }
}