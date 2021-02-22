using System;
using System.Configuration;

namespace Core.AutofacExtensions.Configuration
{
    public class OtherElement : ConfigurationElement
    {

        [ConfigurationProperty("source", IsRequired = true)]
        public string Source
        {
            get
            {
                return (string)this["source"];
            }
        }

        [ConfigurationProperty("interfaceFilters", IsRequired = false)]
        public string InterfaceFilters
        {
            get
            {
                return (string)this["interfaceFilters"];
            }
        }

        [ConfigurationProperty("typeFilters", IsRequired = false)]
        public string TypeFilters
        {
            get
            {
                return (string)this["typeFilters"];
            }
        }
    }
}
