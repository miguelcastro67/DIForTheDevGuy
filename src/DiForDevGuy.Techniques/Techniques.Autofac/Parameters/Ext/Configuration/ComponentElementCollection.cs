using Autofac.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
//using static Ext.Configuration.PropertyElementCollection;

namespace Ext.Configuration
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

    //public class ComponentElementCollection : ConfigurationElementCollection<ComponentElement>
    //{
    //    public ComponentElementCollection()
    //      : base("component")
    //    {
    //    }
    //}

    //public class MetadataElementCollection : NamedConfigurationElementCollection<MetadataElement>
    //{
    //    public MetadataElementCollection()
    //      : base("item", "name")
    //    {
    //    }
    //}

    //public class MetadataElement : ConfigurationElement
    //{
    //    private const string NameAttributeName = "name";
    //    private const string ValueAttributeName = "value";
    //    private const string TypeAttributeName = "type";
    //    internal const string Key = "name";

    //    [ConfigurationProperty("name", IsRequired = true)]
    //    public string Name
    //    {
    //        get
    //        {
    //            return (string)this["name"];
    //        }
    //    }

    //    [ConfigurationProperty("value", IsRequired = true)]
    //    public string Value
    //    {
    //        get
    //        {
    //            return (string)this["value"];
    //        }
    //    }

    //    [ConfigurationProperty("type", IsRequired = false)]
    //    [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
    //    public string Type
    //    {
    //        get
    //        {
    //            return (string)this["type"] ?? typeof(string).FullName;
    //        }
    //    }
    //}

    //public class ParameterElementCollection : NamedConfigurationElementCollection<ParameterElement>
    //{
    //    public ParameterElementCollection()
    //      : base("parameter", "name")
    //    {
    //    }

    //    public IEnumerable<Parameter> ToParameters()
    //    {
    //        // ISSUE: object of a compiler-generated type is created
    //        return (IEnumerable<Parameter>)new ParameterElementCollection().ToParameters();//.\u003CToParameters\u003Ed__4(-2)
    //                                                                                       //    {
    //                                                                                       //  u003C\u003E4__this = this
    //                                                                                       //};
    //    }
    //}

    //public class ParameterElement : ConfigurationElement
    //{
    //    private const string NameAttributeName = "name";
    //    private const string ValueAttributeName = "value";
    //    private const string ListElementName = "list";
    //    private const string DictionaryElementName = "dictionary";
    //    internal const string Key = "name";

    //    [ConfigurationProperty("name", IsRequired = true)]
    //    public string Name
    //    {
    //        get
    //        {
    //            return (string)this["name"];
    //        }
    //    }

    //    [ConfigurationProperty("value", IsRequired = false)]
    //    public string Value
    //    {
    //        get
    //        {
    //            return (string)this["value"];
    //        }
    //    }

    //    [ConfigurationProperty("list", DefaultValue = null, IsRequired = false)]
    //    public ListElementCollection List
    //    {
    //        get
    //        {
    //            return this["list"] as ListElementCollection;
    //        }
    //    }

    //    [ConfigurationProperty("dictionary", DefaultValue = null, IsRequired = false)]
    //    public DictionaryElementCollection Dictionary
    //    {
    //        get
    //        {
    //            return this["dictionary"] as DictionaryElementCollection;
    //        }
    //    }

    //    public object CoerceValue()
    //    {
    //        if (this.List.ElementInformation.IsPresent)
    //            return (object)this.List;
    //        if (this.Dictionary.ElementInformation.IsPresent)
    //            return (object)this.Dictionary;
    //        return (object)this.Value;
    //    }
    //}

    //[TypeConverter(typeof(DictionaryElementCollection.DictionaryElementTypeConverter))]
    //public class DictionaryElementCollection : ConfigurationElementCollection<ListItemElement>
    //{
    //    public DictionaryElementCollection()
    //      : base("item")
    //    {
    //    }

    //    private class DictionaryElementTypeConverter : TypeConverter
    //    {
    //        public override object ConvertTo(
    //          ITypeDescriptorContext context,
    //          CultureInfo culture,
    //          object value,
    //          Type destinationType)
    //        {
    //            Type instantiableType = DictionaryElementCollection.DictionaryElementTypeConverter.GetInstantiableType(destinationType);
    //            DictionaryElementCollection elementCollection = value as DictionaryElementCollection;
    //            if (elementCollection == null || !(instantiableType != (Type)null))
    //                return base.ConvertTo(context, culture, value, destinationType);
    //            IDictionary instance = (IDictionary)Activator.CreateInstance(instantiableType);
    //            Type[] genericArguments = instantiableType.GetGenericArguments();
    //            foreach (ListItemElement listItemElement in (ConfigurationElementCollection<ListItemElement>)elementCollection)
    //            {
    //                if (string.IsNullOrEmpty(listItemElement.Key))
    //                    throw new ConfigurationErrorsException("Key cannot be null in a dictionary element.");
    //                object compatibleType1 = TypeManipulation.ChangeToCompatibleType((object)listItemElement.Key, genericArguments[0]);
    //                object compatibleType2 = TypeManipulation.ChangeToCompatibleType((object)listItemElement.Value, genericArguments[1]);
    //                instance.Add(compatibleType1, compatibleType2);
    //            }
    //            return (object)instance;
    //        }

    //        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    //        {
    //            if (DictionaryElementCollection.DictionaryElementTypeConverter.GetInstantiableType(destinationType) != (Type)null)
    //                return true;
    //            return base.CanConvertTo(context, destinationType);
    //        }

    //        private static Type GetInstantiableType(Type destinationType)
    //        {
    //            if (typeof(IDictionary).IsAssignableFrom(destinationType) || destinationType.IsGenericType && typeof(IDictionary<,>).IsAssignableFrom(destinationType.GetGenericTypeDefinition()))
    //            {
    //                Type[] typeArray1;
    //                if (!destinationType.IsGenericType)
    //                    typeArray1 = new Type[2]
    //                    {
    //          typeof (string),
    //          typeof (object)
    //                    };
    //                else
    //                    typeArray1 = destinationType.GetGenericArguments();
    //                Type[] typeArray2 = typeArray1;
    //                if (typeArray2.Length != 2)
    //                    return (Type)null;
    //                Type c = typeof(Dictionary<,>).MakeGenericType(typeArray2);
    //                if (destinationType.IsAssignableFrom(c))
    //                    return c;
    //            }
    //            return (Type)null;
    //        }
    //    }
    //}

    //public class ListItemElement : ConfigurationElement
    //{
    //    private const string ValueAttributeName = "value";
    //    private const string KeyAttributeName = "key";

    //    [ConfigurationProperty("key", IsRequired = false)]
    //    public string Key
    //    {
    //        get
    //        {
    //            return (string)this["key"];
    //        }
    //    }

    //    [ConfigurationProperty("value", IsRequired = true)]
    //    public string Value
    //    {
    //        get
    //        {
    //            return (string)this["value"];
    //        }
    //    }
    //}

    //public class PropertyElement : ConfigurationElement
    //{
    //    private const string NameAttributeName = "name";
    //    private const string ValueAttributeName = "value";
    //    private const string ListElementName = "list";
    //    private const string DictionaryElementName = "dictionary";
    //    internal const string Key = "name";

    //    [ConfigurationProperty("name", IsRequired = true)]
    //    public string Name
    //    {
    //        get
    //        {
    //            return (string)this["name"];
    //        }
    //    }

    //    [ConfigurationProperty("value", IsRequired = false)]
    //    public string Value
    //    {
    //        get
    //        {
    //            return (string)this["value"];
    //        }
    //    }

    //    [ConfigurationProperty("list", DefaultValue = null, IsRequired = false)]
    //    public ListElementCollection List
    //    {
    //        get
    //        {
    //            return this["list"] as ListElementCollection;
    //        }
    //    }

    //    [ConfigurationProperty("dictionary", DefaultValue = null, IsRequired = false)]
    //    public DictionaryElementCollection Dictionary
    //    {
    //        get
    //        {
    //            return this["dictionary"] as DictionaryElementCollection;
    //        }
    //    }

    //    public object CoerceValue()
    //    {
    //        if (this.List.ElementInformation.IsPresent)
    //            return (object)this.List;
    //        if (this.Dictionary.ElementInformation.IsPresent)
    //            return (object)this.Dictionary;
    //        return (object)this.Value;
    //    }
    //}

    //public class PropertyElementCollection : NamedConfigurationElementCollection<PropertyElement>
    //{
    //    public PropertyElementCollection()
    //      : base("property", "name")
    //    {
    //    }

    //    public IEnumerable<Parameter> ToParameters()
    //    {
    //        // ISSUE: object of a compiler-generated type is created
    //        return (IEnumerable<Parameter>)new PropertyElementCollection().ToParameters();//.\u003CToParameters\u003Ed__4(-2)
    //                                                                                      //    {
    //                                                                                      //  \u003C\u003E4__this = this
    //                                                                                      //};
    //                                                                                      //  }
    //    }

    //    [TypeConverter(typeof(ListElementCollection.ListElementTypeConverter))]
    //    public class ListElementCollection : ConfigurationElementCollection<ListItemElement>
    //    {
    //        public ListElementCollection()
    //          : base("item")
    //        {
    //        }

    //        private class ListElementTypeConverter : TypeConverter
    //        {
    //            public override object ConvertTo(
    //              ITypeDescriptorContext context,
    //              CultureInfo culture,
    //              object value,
    //              Type destinationType)
    //            {
    //                Type instantiableType = ListElementCollection.ListElementTypeConverter.GetInstantiableType(destinationType);
    //                ListElementCollection elementCollection = value as ListElementCollection;
    //                if (elementCollection == null || !(instantiableType != (Type)null))
    //                    return base.ConvertTo(context, culture, value, destinationType);
    //                Type[] genericArguments = instantiableType.GetGenericArguments();
    //                IList instance = (IList)Activator.CreateInstance(instantiableType);
    //                foreach (ListItemElement listItemElement in (ConfigurationElementCollection<ListItemElement>)elementCollection)
    //                    instance.Add(TypeManipulation.ChangeToCompatibleType((object)listItemElement.Value, genericArguments[0]));
    //                return (object)instance;
    //            }

    //            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    //            {
    //                if (ListElementCollection.ListElementTypeConverter.GetInstantiableType(destinationType) != (Type)null)
    //                    return true;
    //                return base.CanConvertTo(context, destinationType);
    //            }

    //            private static Type GetInstantiableType(Type destinationType)
    //            {
    //                if (typeof(IEnumerable).IsAssignableFrom(destinationType))
    //                {
    //                    Type[] typeArray1;
    //                    if (!destinationType.IsGenericType)
    //                        typeArray1 = new Type[1] { typeof(object) };
    //                    else
    //                        typeArray1 = destinationType.GetGenericArguments();
    //                    Type[] typeArray2 = typeArray1;
    //                    if (typeArray2.Length != 1)
    //                        return (Type)null;
    //                    Type c = typeof(List<>).MakeGenericType(typeArray2);
    //                    if (destinationType.IsAssignableFrom(c))
    //                        return c;
    //                }
    //                return (Type)null;
    //            }
    //        }
    //    }

    //    internal class TypeManipulation
    //    {
    //        public static object ChangeToCompatibleType(object value, Type destinationType)
    //        {
    //            if (destinationType == (Type)null)
    //                throw new ArgumentNullException(nameof(destinationType));
    //            if (value == null)
    //            {
    //                if (!destinationType.IsValueType)
    //                    return (object)null;
    //                return Activator.CreateInstance(destinationType);
    //            }
    //            TypeConverter converter1 = TypeDescriptor.GetConverter(value.GetType());
    //            if (converter1.CanConvertTo(destinationType))
    //                return converter1.ConvertTo(value, destinationType);
    //            if (destinationType.IsInstanceOfType(value))
    //                return value;
    //            TypeConverter converter2 = TypeDescriptor.GetConverter(destinationType);
    //            if (converter2.CanConvertFrom(value.GetType()))
    //                return converter2.ConvertFrom(value);
    //            if (value is string)
    //            {
    //                MethodInfo method = destinationType.GetMethod("TryParse", BindingFlags.Static | BindingFlags.Public);
    //                if (method != (MethodInfo)null)
    //                {
    //                    object[] parameters = new object[2] { value, null };
    //                    if ((bool)method.Invoke((object)null, parameters))
    //                        return parameters[1];
    //                }
    //            }
    //            throw new ConfigurationErrorsException(); // string.Format((IFormatProvider)CultureInfo.CurrentCulture, ConfigurationSettingsReaderResources.TypeConversionUnsupported, (object)value.GetType(), (object)destinationType));
    //        }
    //    }
    //}

    //[TypeConverter(typeof(ListElementCollection.ListElementTypeConverter))]
    //public class ListElementCollection : ConfigurationElementCollection<ListItemElement>
    //{
    //    public ListElementCollection()
    //      : base("item")
    //    {
    //    }

    //    private class ListElementTypeConverter : TypeConverter
    //    {
    //        public override object ConvertTo(
    //          ITypeDescriptorContext context,
    //          CultureInfo culture,
    //          object value,
    //          Type destinationType)
    //        {
    //            Type instantiableType = ListElementCollection.ListElementTypeConverter.GetInstantiableType(destinationType);
    //            ListElementCollection elementCollection = value as ListElementCollection;
    //            if (elementCollection == null || !(instantiableType != (Type)null))
    //                return base.ConvertTo(context, culture, value, destinationType);
    //            Type[] genericArguments = instantiableType.GetGenericArguments();
    //            IList instance = (IList)Activator.CreateInstance(instantiableType);
    //            foreach (ListItemElement listItemElement in (ConfigurationElementCollection<ListItemElement>)elementCollection)
    //                instance.Add(TypeManipulation.ChangeToCompatibleType((object)listItemElement.Value, genericArguments[0]));
    //            return (object)instance;
    //        }

    //        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    //        {
    //            if (ListElementCollection.ListElementTypeConverter.GetInstantiableType(destinationType) != (Type)null)
    //                return true;
    //            return base.CanConvertTo(context, destinationType);
    //        }

    //        private static Type GetInstantiableType(Type destinationType)
    //        {
    //            if (typeof(IEnumerable).IsAssignableFrom(destinationType))
    //            {
    //                Type[] typeArray1;
    //                if (!destinationType.IsGenericType)
    //                    typeArray1 = new Type[1] { typeof(object) };
    //                else
    //                    typeArray1 = destinationType.GetGenericArguments();
    //                Type[] typeArray2 = typeArray1;
    //                if (typeArray2.Length != 1)
    //                    return (Type)null;
    //                Type c = typeof(List<>).MakeGenericType(typeArray2);
    //                if (destinationType.IsAssignableFrom(c))
    //                    return c;
    //            }
    //            return (Type)null;
    //        }
    //    }
    //}
}