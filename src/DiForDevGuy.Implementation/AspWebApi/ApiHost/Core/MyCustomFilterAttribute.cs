using Lib.Abstractions;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Filters;

namespace ApiHost.Core
{
    public class MyCustomFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            // scope must be created if dependency is IntancePerRequest
            using (IDependencyScope scope = GlobalConfiguration.Configuration.DependencyResolver.BeginScope())
            {
                var logger = scope.GetService(typeof(ILogger)) as ILogger;

                // only if transient scope registration for dependency
                //var logger = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ILogger)) as ILogger;
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}