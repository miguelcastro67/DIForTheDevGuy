using Lib.Abstractions;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WinFormClient
{
    public partial class AvengersForm : Form
    {
        public AvengersForm(ISuperheroService superheroService)
        {
            InitializeComponent();

            var heros = superheroService.GetAvengers();
            grdAvengers.DataSource = heros;
        }
    }
}
