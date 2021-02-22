using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Core.CastleWindsorExtensions.Configuration
{
    public class ComponentElement : ConfigurationElement
    {
        internal const string Key = "type";

        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get
            {
                return (string)this["type"];
            }
        }

        [ConfigurationProperty("service", IsRequired = false)]
        public string Service
        {
            get
            {
                return (string)this["service"];
            }
        }

        [ConfigurationProperty("instance-scope", IsRequired = false, DefaultValue = "")]
        public string InstanceScope
        {
            get
            {
                return (string)this["instance-scope"];
            }
        }

        [ConfigurationProperty("instance-ownership", IsRequired = false)]
        public string Ownership
        {
            get
            {
                return (string)this["instance-ownership"];
            }
        }
    }
}