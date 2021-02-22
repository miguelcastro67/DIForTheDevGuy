using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace WcfClient
{
    public class SuperheroClient : ClientBase<ISuperheroService>, ISuperheroService, IDisposable
    {
        public Hero GetAvenger(string name)
        {
            return Channel.GetAvenger(name);
        }

        public IEnumerable<Hero> GetAvengers()
        {
            return Channel.GetAvengers();
        }

        // this is for this demo - ClientBase already implements IDisposable but does not provide virtual
        // so problem is very invisible if you use client-side DI incorrectly
        public void Dispose()
        {
            Console.WriteLine("Proxy disposed.");
        }
    }
}
