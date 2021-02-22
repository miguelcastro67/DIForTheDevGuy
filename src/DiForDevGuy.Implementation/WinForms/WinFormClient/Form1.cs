using Autofac;
using Lib;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WinFormClient
{
    public partial class Form1 : Form
    {
        public Form1(IComponentLocator componentLocator)
        {
            InitializeComponent();

            _ComponentLocator = componentLocator;
        }
        
        IComponentLocator _ComponentLocator;

        // A custom locator is used here like the on-demand factory class 
        // which would seperate the forms from the actual container.
        // An Extensions project would house all of this.

        private void btnAvengers_Click(object sender, EventArgs e)
        {
            var avengersForm = _ComponentLocator.ResolveComponent<AvengersForm>();
            if (avengersForm != null)
                avengersForm.Show();
        }

        private void btnAvenger_Click(object sender, EventArgs e)
        {
            var avengerForm = _ComponentLocator.ResolveComponent<AvengerForm>();
            if (avengerForm != null)
                avengerForm.Show();
        }
    }
}
