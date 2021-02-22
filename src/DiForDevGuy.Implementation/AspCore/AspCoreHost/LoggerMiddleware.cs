using Lib.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreHost
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _Next;
        private readonly ILogger _Logger;

        public LoggerMiddleware(RequestDelegate next, ILogger logger)
        {
            this._Next = next;
            _Logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            //ILifetimeScope scope = context.GetAutofacLifetimeScope();
            // can use this to resolve other stuff in same lifetime scope as the owin pipeline

            _Logger.Log("Inside the 'Invoke' method of the 'LoggerMiddleware' middleware.");

            await this._Next.Invoke(context).ConfigureAwait(false);
        }
    }
}
