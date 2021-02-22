using Lib.Abstractions;
using System;

namespace Lib
{
    public class ConsoleLogger : LoggerBase
    {
        public override void Log(string message, params string[] args)
        {
            string messageToLog = string.Format(message, args);

            Console.WriteLine(messageToLog);
        }
    }
}
