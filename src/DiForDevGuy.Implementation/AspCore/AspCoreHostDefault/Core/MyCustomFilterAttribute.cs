using Lib.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreHostDefault.Core
{
    public class MyCustomFilterAttribute : Attribute, IFilterFactory
    {
        public MyCustomFilterAttribute(string value1, string value2)
        {
            Value1 = value1;
            Value2 = value2;
        }

        public string Value1 { get; set; }
        public string Value2 { get; set; }

        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            MyCustomFilter myCustomFilter = serviceProvider.GetService(typeof(MyCustomFilter)) as MyCustomFilter;

            myCustomFilter.Value1 = Value1;
            myCustomFilter.Value2 = Value2;
            
            return myCustomFilter;
        }
    }
}
