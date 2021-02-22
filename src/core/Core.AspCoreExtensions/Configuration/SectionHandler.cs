using System;
using System.Configuration;

namespace Core.AspCoreExtensions.Configuration
{
    public class SectionHandler : ConfigurationSection
    {
        [ConfigurationProperty("modules", IsRequired = false)]
        public ModulesElementCollection Modules
        {
            get
            {
                return (ModulesElementCollection)this["modules"];
            }
        }

        [ConfigurationProperty("components", IsRequired = false)]
        public ComponentElementCollection Components
        {
            get
            {
                return (ComponentElementCollection)this["components"];
            }
        }
    }
}