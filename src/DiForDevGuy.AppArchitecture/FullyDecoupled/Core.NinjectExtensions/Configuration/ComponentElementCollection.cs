﻿using System;
using System.Configuration;

namespace Core.NinjectExtensions.Configuration
{
    [ConfigurationCollection(typeof(ComponentElement), AddItemName = "component")]
    public class ComponentElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ComponentElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ComponentElement)element).Type;
        }
    }
}