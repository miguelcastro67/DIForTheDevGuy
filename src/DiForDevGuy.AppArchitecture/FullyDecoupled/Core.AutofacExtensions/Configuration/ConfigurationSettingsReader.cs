using Autofac;
using Autofac.Builder;
using Autofac.Core;
using System;
using System.Configuration;

namespace Core.AutofacExtensions.Configuration
{
    public class ConfigurationSettingsReader : Module
    {
        public ConfigurationSettingsReader()
            : this("autofac")
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

        protected override void Load(ContainerBuilder builder)
        {
            SectionHandler config = this.SectionHandler;
            if (config != null)
            {
                foreach (ModuleElement moduleElement in config.Modules)
                {
                    Type moduleType = Type.GetType(moduleElement.Type);
                    if (moduleType != null)
                    {
                        Module module = Activator.CreateInstance(moduleType) as Module;
                        if (module != null)
                        {
                            builder.RegisterModule(module);
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
                            builder.RegisterType(componentType).As(serviceType);
                        else if (componentElement.InstanceScope == "singleinstance")
                            builder.RegisterType(componentType).As(serviceType).SingleInstance();
                        else if (componentElement.InstanceScope == "perlifetimescope")
                            builder.RegisterType(componentType).As(serviceType).InstancePerLifetimeScope();
                        else if (componentElement.InstanceScope == "instanceperrequest")
                            builder.RegisterType(componentType).As(serviceType).InstancePerRequest();
                    }
                    else
                    {
                        if (componentElement.InstanceScope == "" || componentElement.InstanceScope == "perdepedency")
                            builder.RegisterType(componentType);
                        else if (componentElement.InstanceScope == "singleinstance")
                            builder.RegisterType(componentType).SingleInstance();
                        else if (componentElement.InstanceScope == "perlifetimescope")
                            builder.RegisterType(componentType).InstancePerLifetimeScope();
                        else if (componentElement.InstanceScope == "perrequest")
                            builder.RegisterType(componentType).InstancePerRequest();
                    }
                }
            }
            else
                throw new ApplicationException("Cannot find configuration section 'autofac'.");
        }
    }
}