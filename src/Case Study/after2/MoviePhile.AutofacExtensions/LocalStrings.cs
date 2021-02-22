using Core.Common;
using System.Linq;

namespace MoviePhile.AutofacExtensions
{
    public class LocalStrings : ILocalStrings
    {
        string ILocalStrings.Title => "Autofac DI";
    }
}
