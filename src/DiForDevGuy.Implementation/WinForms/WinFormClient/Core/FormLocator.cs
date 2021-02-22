using Autofac;
using Autofac.Core;
using Lib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WinFormClient.Core
{
    public class FormLocator : IComponentLocator
    {
        public FormLocator(ILifetimeScope container)
        {
            _Container = container;
        }

        ILifetimeScope _Container;

        T IComponentLocator.ResolveComponent<T>(params object[] args)
        {
            List<Parameter> parameters = new List<Parameter>();
            if (args != null)
            {
                foreach (object arg in args)
                {
                    Parameter parameter = arg as Parameter;
                    if (arg != null)
                        parameters.Add(parameter);
                }
            }

            //return Program.Container.Resolve<T>(parameters.ToArray());
            return _Container.Resolve<T>(parameters.ToArray());
        }
    }
}
