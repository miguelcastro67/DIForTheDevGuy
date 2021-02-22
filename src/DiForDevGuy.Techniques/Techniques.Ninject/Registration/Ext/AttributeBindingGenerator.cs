using Ninject.Extensions.Conventions.BindingGenerators;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ext
{
    public class AttributeBindingGenerator : IBindingGenerator
    {
        IEnumerable<IBindingWhenInNamedWithOrOnSyntax<object>> IBindingGenerator.CreateBindings(
            Type type, IBindingRoot bindingRoot)
        {
            RegisterAttribute registerAttr = type.GetCustomAttribute<RegisterAttribute>(true);
            if (registerAttr != null)
            {
                if (registerAttr.As != null)
                {
                    yield return bindingRoot.Bind(registerAttr.As).To(type);
                }
                else
                    yield break;
            }
            else
                yield break;
        }
    }
}
