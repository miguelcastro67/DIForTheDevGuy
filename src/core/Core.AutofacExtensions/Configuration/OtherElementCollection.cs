using System;
using System.Configuration;

namespace Core.AutofacExtensions.Configuration
{
    [ConfigurationCollection(typeof(OtherElement), AddItemName = "other")]
    public class OtherElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new OtherElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((OtherElement)element).Source;
        }
    }
}
