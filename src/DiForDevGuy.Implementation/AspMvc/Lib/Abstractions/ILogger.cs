using System;

namespace Lib.Abstractions
{
    public interface ILogger
    {
        void Log(string message, params string[] args);
    }
}
