using System;
using System.Collections.Generic;

namespace Lib.Abstractions
{
    public interface ISuperheroService
    {
        Hero GetAvenger(string name);
        IEnumerable<Hero> GetAvengers();
    }
}
