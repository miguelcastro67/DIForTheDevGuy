using System;
using System.Configuration;

namespace Ext.Configuration
{
    [ConfigurationCollection(typeof(ModuleElement), AddItemName = "module")]
    public class ModulesElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ModuleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ModuleElement)element).Type;
        }
    }
}