using System;
using System.Collections.Specialized;

namespace Lib.Abstractions
{
    public interface ILogger
    {
        string Name { get; }
        string Type { get; }
        bool Enabled { get; }
        void Initialize(string name, NameValueCollection config);
        void Log(string message, params string[] args);
    }
}
