using Autofac.Core.Activators.Reflection;
using System;
using System.Linq;
using System.Reflection;

namespace Ext
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class AwesomeConstructorAttribute : Attribute
    {
    }

    public class AwesomeConstructorFinder : IConstructorFinder
    {
        ConstructorInfo[] IConstructorFinder.FindConstructors(Type targetType)
        {
            ConstructorInfo constructorToResolve = null;

            ConstructorInfo[] constructors = targetType.GetConstructors();

            foreach (ConstructorInfo constructor in constructors)
            {
                object[] attributes = constructor.GetCustomAttributes(typeof(AwesomeConstructorAttribute), false);
                if (attributes != null && attributes.Count() > 0)
                {
                    constructorToResolve = constructor;
                    break;
                }
            }

            return new ConstructorInfo[] { constructorToResolve };
        }
    }


}
