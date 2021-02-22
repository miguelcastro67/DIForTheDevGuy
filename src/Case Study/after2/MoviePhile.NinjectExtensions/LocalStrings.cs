using Core.Common;
using System.Linq;

namespace MoviePhile.NinjectExtensions
{
    public class LocalStrings : ILocalStrings
    {
        string ILocalStrings.Title => "Ninject DI";
    }
}
