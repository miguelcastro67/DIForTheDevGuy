using Ninject;
using Ninject.Modules;
using System;
using System.Configuration;

namespace Ext.Configuration
{
    public class ConfigurationSettingsReader : NinjectModule
    {
        public override void Load()
        {
            SectionHandler config = ConfigurationManager.GetSection("ninject") as SectionHandler;
            if (config != null)
            {
                foreach (ModuleElement moduleElement in config.Modules)
                {
                    Type moduleType = Type.GetType(moduleElement.Type);
                    if (moduleType != null)
                    {
                        NinjectModule module = Activator.CreateInstance(moduleType) as NinjectModule;
                        if (module != null)
                        {
                            this.Kernel.Load(module);
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
                            this.Kernel.Bind(serviceType).To(componentType);
                        else if (componentElement.InstanceScope == "singleton")
                            this.Kernel.Bind(serviceType).To(componentType).InSingletonScope();
                        else if (componentElement.InstanceScope == "lifetimescope")
                            this.Kernel.Bind(serviceType).To(componentType).InScope(x => LifetimeScope.Current);
                    }
                }
            }
            else
                throw new ApplicationException("Cannot find configuration section 'ninject'.");
        }
    }
}