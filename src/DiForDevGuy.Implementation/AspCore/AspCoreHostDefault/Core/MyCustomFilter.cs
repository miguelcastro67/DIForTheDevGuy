using Lib.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace AspCoreHostDefault.Core
{
    public class MyCustomFilter : IActionFilter
    {
        public MyCustomFilter(ILogger logger)
        {
            _Logger = logger;
        }

        public ILogger _Logger { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _Logger.Log("In OnActionExecuting");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _Logger.Log("In OnActionExecuted");
        }
    }
}
