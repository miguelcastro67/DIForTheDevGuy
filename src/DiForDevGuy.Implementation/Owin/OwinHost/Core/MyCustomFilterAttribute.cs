using Lib.Abstractions;
using System;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Filters;

namespace OwinHost.Core
{
    public class MyCustomFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            var config = actionContext.RequestContext.Configuration;
            // scope must be created if dependency is IntancePerRequest
            using (IDependencyScope scope = config.DependencyResolver.BeginScope())
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
