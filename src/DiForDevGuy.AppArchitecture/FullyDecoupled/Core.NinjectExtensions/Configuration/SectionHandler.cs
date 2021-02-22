using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Core.NinjectExtensions.Configuration
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
