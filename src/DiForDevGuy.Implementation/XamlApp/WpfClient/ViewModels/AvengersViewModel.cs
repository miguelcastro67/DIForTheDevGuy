using Lib;
using Lib.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using WpfClient.Core;

namespace WpfClient.ViewModels
{   
    public class AvengersViewModel : ViewModelBase
    {
        public AvengersViewModel(ISuperheroService superheroService)
        {
            _HeroList = superheroService.GetAvengers();
            SelectAvengerCommand = new DelegateCommand<string>(SelectAvengerCommand_Execute, (name) => { return true; });
        }

        IEnumerable<Hero> _HeroList;
        
        public event EventHandler<AvengerSelectedEventArgs> AvengerSelected;

        public DelegateCommand<string> SelectAvengerCommand { get; private set; }
        
        public IEnumerable<Hero> AvengersModel
        {
            get { return _HeroList; }
        }

        void SelectAvengerCommand_Execute(string name)
        {
            this.AvengerSelected?.Invoke(this, new AvengerSelectedEventArgs(name));
        }
    }
}
