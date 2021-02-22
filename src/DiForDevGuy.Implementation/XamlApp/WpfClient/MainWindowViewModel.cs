using Autofac;
using Lib;
using System;
using System.Linq;
using WpfClient.Core;
using WpfClient.ViewModels;

namespace WpfClient
{   
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(IComponentLocator componentLocator, 
                                   AvengersViewModel avengersViewModel)
        {
            _ComponentLocator = componentLocator;
            _AvengersViewModel = avengersViewModel;
            _AvengersViewModel.AvengerSelected += OnAvengerSelected;
            CurrentViewModel = _AvengersViewModel;
            RefreshCommand = new DelegateCommand<object>(RefreshCommand_Execute, (arg) => { return true; });
        }

        IComponentLocator _ComponentLocator;
        AvengersViewModel _AvengersViewModel;

        ViewModelBase _CurrentViewModel;

        public DelegateCommand<object> RefreshCommand { get; private set; }
        
        public ViewModelBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set
            {
                _CurrentViewModel = value;
                OnPropertyChanged();
            }
        }

        void OnAvengerSelected(object sender, AvengerSelectedEventArgs e)
        {
            CurrentViewModel = 
                _ComponentLocator.ResolveComponent<AvengerViewModel>(
                    new NamedParameter("superheroName", e.SuperheroName));

            #region manual instantiation comparison
            //CurrentViewModel = new AvengerViewModel(
            //    new SuperheroService(
            //        new AvengerRepository(
            //            new Logger()), 
            //        new Logger()), 
            //    e.SuperheroName);
            #endregion
        }

        void RefreshCommand_Execute(object arg)
        {
            CurrentViewModel = _AvengersViewModel;
        }
    }
}
