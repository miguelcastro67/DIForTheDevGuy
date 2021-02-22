using System;
using System.Linq;
public class AvengerSelectedEventArgs : EventArgs
{
    public AvengerSelectedEventArgs(string superheroName)
    {
        SuperheroName = superheroName;
    }

    public string SuperheroName { get; set; }
}