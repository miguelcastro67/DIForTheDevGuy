using Lib;
using Lib.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using WebFormClient.Core;

namespace WebFormClient
{
    //public partial class Avenger : PageBase
    public partial class Avenger : System.Web.UI.Page
    {
        public ISuperheroService _SuperheroService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string value = Request.QueryString["name"];
            if (!string.IsNullOrWhiteSpace(value))
            {
                detAvenger.DataSource = new List<Hero>() { _SuperheroService.GetAvenger(value) };
                detAvenger.DataBind();
            }
        }
    }
}