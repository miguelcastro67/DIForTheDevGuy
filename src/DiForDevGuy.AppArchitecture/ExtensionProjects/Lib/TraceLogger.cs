using Lib.Abstractions;
using System;
using System.Diagnostics;

namespace Lib
{
    public class TraceLogger : LoggerBase
    {
        public override void Log(string message, params string[] args)
        {
            string messageToLog = string.Format(message, args);

            Trace.WriteLine(messageToLog);
        }
    }
}
