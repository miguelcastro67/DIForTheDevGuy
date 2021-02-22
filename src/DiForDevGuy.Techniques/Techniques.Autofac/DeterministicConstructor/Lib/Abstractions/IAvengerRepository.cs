using System;
using System.Collections.Generic;

namespace Lib.Abstractions
{
    public interface IAvengerRepository
    {
        IEnumerable<Hero> FetchAll();
        Hero Fetch(string name);
    }
}
