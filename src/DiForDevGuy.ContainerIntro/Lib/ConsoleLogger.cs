using Lib.Abstractions;
using System;

namespace Lib
{
    public class ConsoleLogger : ILogger
    {
        void  ILogger.Log(string message, params string[] args)
        {
            string messageToLog = string.Format(message, args);

            Console.WriteLine(messageToLog);
        }
    }
}
