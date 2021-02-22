using Lib.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Lib.Abstractions
{
    public interface IConfigurationFactory
    {
        IEnumerable<ILogger> GetLoggers();
    }
}
