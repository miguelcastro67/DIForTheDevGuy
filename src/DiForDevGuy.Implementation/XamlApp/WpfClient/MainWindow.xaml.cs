using Autofac;
using System;
using System.Linq;
using System.Windows;

namespace WpfClient
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var viewModel = Program.Container.Resolve<MainWindowViewModel>();

            this.DataContext = viewModel;
        }
    }
}
