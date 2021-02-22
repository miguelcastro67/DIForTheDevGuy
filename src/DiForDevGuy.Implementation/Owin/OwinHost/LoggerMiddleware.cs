using Autofac;
using Autofac.Integration.Owin;
using Lib.Abstractions;
using Microsoft.Owin;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OwinHost
{
    public class LoggerMiddleware : OwinMiddleware
    {
        private readonly ILogger _Logger;

        public LoggerMiddleware(OwinMiddleware next, ILogger logger) : base(next)
        {
            _Logger = logger;
        }

        public override async Task Invoke(IOwinContext context)
        {
            ILifetimeScope scope = context.GetAutofacLifetimeScope();
            // can use this to resolve other stuff in same lifetime scope as the owin pipeline

            _Logger.Log("Inside the 'Invoke' method of the 'LoggerMiddleware' middleware.");
           
            await Next.Invoke(context);
        }
    }
}
