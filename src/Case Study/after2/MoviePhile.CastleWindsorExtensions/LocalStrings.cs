using Core.Common;
using System.Linq;

namespace MoviePhile.CastleWindsorExtensions
{
    public class LocalStrings : ILocalStrings
    {
        string ILocalStrings.Title => "Castle Windsor DI";
    }
}
