using System;
using System.Configuration;

namespace Lib.Configuration
{
    public class DiWrappersConfigurationSection : ConfigurationSection
    {

        [ConfigurationProperty("loggers", IsRequired = false)]
        public ProviderSettingsCollection Loggers
        {
            get { return (ProviderSettingsCollection)base["loggers"]; }
            set { base["loggers"] = value; }
        }
    }
}
 