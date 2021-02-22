using System;
using System.Configuration;
using Unity;
using Unity.Extension;

namespace Core.UnityExtensions.Configuration
{
    public class ConfigurationSettingsReader : UnityContainerExtension
    {
        public ConfigurationSettingsReader()
            : this("unity")
        {            
        }

        public ConfigurationSettingsReader(string sectionName)
        {
            _SectionName = sectionName;
            this.SectionHandler = (SectionHandler)ConfigurationManager.GetSection(sectionName);
        }
        
        public ConfigurationSettingsReader(string sectionName, string configurationFile)
        {
            _SectionName = sectionName;
            this.SectionHandler = SectionHandler.Deserialize(configurationFile, sectionName);
        }
        
        string _SectionName = string.Empty;

        public SectionHandler SectionHandler { get; protected set; }

        protected override void Initialize()
        {
            SectionHandler config = this.SectionHandler;
            if (config != null)
            {
                foreach (ModuleElement moduleElement in config.Modules)
                {
                    Type moduleType = Type.GetType(moduleElement.Type);
                    if (moduleType != null)
                    {
                        UnityContainerExtension module = Activator.CreateInstance(moduleType) as UnityContainerExtension;
                        if (module != null)
                        {
                            Container.AddExtension(module);
                        }
                        else
                            throw new ApplicationException(string.Format("Configured module type '{0}' cannot be instantiated.", moduleType.FullName));
                    }
                    else
                        throw new ApplicationException(string.Format("Configured module type '{0}' cannot be resolved.", moduleElement.Type));
                }

                foreach (ComponentElement componentElement in config.Components)
                {
                    Type componentType = Type.GetType(componentElement.Type);
                    if (componentType == null)
                        throw new ApplicationException(string.Format("Configured component type '{0}' cannot be resolved.", componentElement.Type));
                    
                    if (!string.IsNullOrWhiteSpace(componentElement.Service))
                    {
                        Type serviceType = Type.GetType(componentElement.Service);
                        if (serviceType == null)
                            throw new ApplicationException(string.Format("Configured service type '{0}' cannot be resolved.", componentElement.Service));

                        if (componentElement.InstanceScope == "" || componentElement.InstanceScope == "perdependency")
                            Container.RegisterType(serviceType, componentType);
                        else if (componentElement.InstanceScope == "singleinstance")
                            Container.RegisterSingleton(serviceType, componentType);
                        else if (componentElement.InstanceScope == "perlifetimescope")
                            Container.RegisterType(serviceType, componentType);
                    }
                    else
                    {
                        // Unity does not require registration for concrete classes only
                    }
                }
            }
            else
                throw new ApplicationException("Cannot find configuration section 'unity'.");
        }
    }
}