using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;

namespace Core.AspCoreExtensions.Configuration
{
    public class ConfigurationSettingsReader : AspCoreDIModule
    {
        public ConfigurationSettingsReader(string section)
        {
            _SectionName = section;
        }
        
        string _SectionName = string.Empty;

        public override void Load(IServiceCollection services)
        {
            SectionHandler config = ConfigurationManager.GetSection(_SectionName) as SectionHandler;
            if (config != null)
            {
                foreach (ModuleElement moduleElement in config.Modules)
                {
                    Type moduleType = Type.GetType(moduleElement.Type);
                    if (moduleType != null)
                    {
                        AspCoreDIModule module = Activator.CreateInstance(moduleType) as AspCoreDIModule;
                        if (module != null)
                        {
                            services.RegisterModule(module);
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
                            services.AddTransient(serviceType, componentType);
                        else if (componentElement.InstanceScope == "singleton")
                            services.AddSingleton(serviceType, componentType);
                        else if (componentElement.InstanceScope == "lifetimescope")
                            services.AddScoped(serviceType, componentType);
                    }
                }
            }
            else
                throw new ApplicationException("Cannot find configuration section 'ninject'.");
        }
    }
}