using Lib.Abstractions;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WinFormClient
{
    public partial class AvengerForm : Form
    {
        public AvengerForm(ISuperheroService superheroService)
        {
            InitializeComponent();

            _SuperheroService = superheroService;
        }

        ISuperheroService _SuperheroService;

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {
                var hero = _SuperheroService.GetAvenger(txtName.Text);
                if (hero != null)
                {
                    lblSuperheroName.Text = hero.SuperheroName;
                    lblRealName.Text = hero.RealName;
                    lblPower.Text = hero.Power;
                }
            }
        }
    }
}
