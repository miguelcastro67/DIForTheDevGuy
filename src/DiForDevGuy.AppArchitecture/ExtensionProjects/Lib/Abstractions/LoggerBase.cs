using System;
using System.Collections.Specialized;

namespace Lib.Abstractions
{
    public abstract class LoggerBase : ILogger
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public bool Enabled { get; private set; } = false;

        public virtual void Initialize(string name, NameValueCollection config)
        {
            Name = name;
            Type = this.GetType().FullName + "," + this.GetType().Assembly.GetName().Name;

            string strEnabled = config["enabled"];
            if (!string.IsNullOrWhiteSpace(strEnabled))
                Enabled = Convert.ToBoolean(strEnabled);
        }

        public abstract void Log(string message, params string[] args);
    }
}
