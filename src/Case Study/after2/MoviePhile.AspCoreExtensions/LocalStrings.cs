using Core.Common;
using System.Linq;

namespace MoviePhile.AspCoreExtensions
{
    public class LocalStrings : ILocalStrings
    {
        string ILocalStrings.Title => "ASP Core DI";
    }
}
