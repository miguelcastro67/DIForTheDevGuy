using Lib.Abstractions;
using System;
using System.Linq;
using WebFormClient.Core;

namespace WebFormClient
{
    //public partial class Avengers : PageBase
    public partial class Avengers : System.Web.UI.Page
    {
        public ISuperheroService _SuperheroService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            grdAvengers.DataSource = _SuperheroService.GetAvengers();
            grdAvengers.DataBind();
        }
    }
}