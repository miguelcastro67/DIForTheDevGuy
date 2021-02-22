using Lib.Abstractions;
using System;
using System.Diagnostics;

namespace Lib
{
    public class Logger : ILogger
    {
        void  ILogger.Log(string message, params object[] args)
        {
            string messageToLog = string.Format(message, args);

            Trace.WriteLine(messageToLog);
        }
    }
}
