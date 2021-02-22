using System;
using System.Collections.Generic;

namespace Lib.Abstractions
{
    public interface IRepository
    {
        IEnumerable<Hero> FetchAll();
        Hero Fetch(string name);
    }
}
