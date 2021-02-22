using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Features.ResolveAnything;
using System;
using System.Configuration;
using System.Linq;

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

                foreach (OtherElement otherElement in config.Others)
                {
                    switch (otherElement.Source)
                    {
                        case "AnyConcreteTypeNotAlreadyRegisteredSource":

                            string[] arrTypeFilters = new string[] { };
                            string typeFiltersValue = otherElement.TypeFilters;
                            if (!string.IsNullOrWhiteSpace(typeFiltersValue))
                                arrTypeFilters = typeFiltersValue.Split(',');

                            string[] arrInterfaceFilters = new string[] { };
                            string interfaceFiltersValue = otherElement.InterfaceFilters;
                            if (!string.IsNullOrWhiteSpace(interfaceFiltersValue))
                                arrInterfaceFilters = interfaceFiltersValue.Split(',');

                            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource(t =>
                            {
                                bool useType = false;

                                if (arrTypeFilters.Length > 0)
                                {
                                    foreach (string typeFilter in arrTypeFilters)
                                    {
                                        var filterMet = (t.Name == typeFilter);
                                        if (filterMet)
                                        {
                                            useType = true;
                                            break;
                                        }
                                    }
                                }

                                if (!useType && arrInterfaceFilters.Length > 0)
                                {
                                    foreach (string interfaceFilter in arrInterfaceFilters)
                                    {
                                        var filterMet = t.GetInterfaces()?.FirstOrDefault(i => i.Name == interfaceFilter) != null;
                                        if (filterMet)
                                        {
                                            useType = true;
                                            break;
                                        }
                                    }
                                }

                                return useType;
                            }));

                            break;
                    }
                }
            }
            else
                throw new ApplicationException("Cannot find configuration section 'autofac'.");
        }
    }
}