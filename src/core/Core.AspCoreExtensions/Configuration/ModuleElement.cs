using System;
using System.Configuration;

namespace Core.AspCoreExtensions.Configuration
{
    public class ModuleElement : ConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return (string)base["type"]; }
            set { base["type"] = value; }
        }
    }
}
