using System;
using System.Linq;
using System.Web.UI;

namespace WebFormClient
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkAvenger_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Replace(" ", "");
            if (txtName.Text != string.Empty)
                Response.Redirect("~/Avenger.aspx?name=" + name);
        }
    }
}