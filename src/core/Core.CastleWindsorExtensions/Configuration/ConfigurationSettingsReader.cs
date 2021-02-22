using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Configuration;

namespace Core.CastleWindsorExtensions.Configuration
{
    public class ConfigurationSettingsReader : IWindsorInstaller
    {
        public ConfigurationSettingsReader()
            : this("castleWindsor")
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

        void IWindsorInstaller.Install(IWindsorContainer container, IConfigurationStore store)
        {
            SectionHandler config = this.SectionHandler;
            if (config != null)
            {
                foreach (ModuleElement moduleElement in config.Modules)
                {
                    Type moduleType = Type.GetType(moduleElement.Type);
                    if (moduleType != null)
                    {
                        IWindsorInstaller module = Activator.CreateInstance(moduleType) as IWindsorInstaller;
                        if (module != null)
                        {
                            container.Install(module);
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

                        if (componentElement.InstanceScope == "" || componentElement.InstanceScope == "transient")
                            container.Register(Component.For(serviceType).ImplementedBy(componentType));
                        else if (componentElement.InstanceScope == "singleinstance")
                            container.Register(Component.For(serviceType).ImplementedBy(componentType).LifestyleSingleton());
                        else if (componentElement.InstanceScope == "lifetimescope")
                            container.Register(Component.For(serviceType).ImplementedBy(componentType).LifestyleScoped());
                    }
                    else
                    {
                        // Castle Windsor does not require registration for concrete classes only
                    }
                }
            }
            else
                throw new ApplicationException("Cannot find configuration section 'castleWindsor'.");
        }
    }
}