using Core.Common;
using Lib.Abstractions;
using Lib.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Lib
{
    public class ConfigurationFactory : IConfigurationFactory
    {
        public ConfigurationFactory(ITypeActivator typeActivator)
        {
            _TypeActivator = typeActivator;
        }

        ITypeActivator _TypeActivator = null;
        DiWrappersConfigurationSection _Config = null;
        IEnumerable<ILogger> _Loggers = null;

        IEnumerable<ILogger> IConfigurationFactory.GetLoggers()
        {
            if (_Loggers == null)
            {
                List<ILogger> loggers = new List<ILogger>();

                var config = GetConfig();
                if (config != null)
                {
                    ProviderSettingsCollection loggerProviders = config.Loggers;
                    foreach (ProviderSettings providerElement in loggerProviders)
                    {
                        bool enabled = false;
                        string enabledValue = providerElement.Parameters["enabled"];
                        if (!string.IsNullOrWhiteSpace(enabledValue) && enabledValue.ToLower() == "true")
                            enabled = true;

                        if (enabled)
                        {
                            Type providerType = Type.GetType(providerElement.Type);
                            if (providerType != null)
                            {
                                //ILogger logger = Activator.CreateInstance(providerType) as ILogger;
                                ILogger logger = _TypeActivator.CreateInstance<ILogger>(providerType);
                                if (logger != null)
                                {
                                    logger.Initialize(providerElement.Name, providerElement.Parameters);
                                    loggers.Add(logger);
                                }
                            }
                        }
                    }
                }

                _Loggers = loggers;
            }

            return _Loggers;
        }

        DiWrappersConfigurationSection GetConfig()
        {
            if (_Config == null)
                _Config = ConfigurationManager.GetSection("diWrappers") as DiWrappersConfigurationSection;

            return _Config;
        }

    }
}
