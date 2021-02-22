using System;
using System.Linq;

namespace MoviePhile.Web.Model
{
    public class UpdateMovieInfoModel
    {
        public int MovieId { get; set; }
        public string Name { get; set; }
        public int GenreId { get; set; }
    }
}
