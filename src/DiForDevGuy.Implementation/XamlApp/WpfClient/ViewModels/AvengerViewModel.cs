using Lib;
using Lib.Abstractions;
using System;
using System.Linq;
using WpfClient.Core;

namespace WpfClient.ViewModels
{
    public class AvengerViewModel : ViewModelBase
    {
        public AvengerViewModel(ISuperheroService superheroService, string superheroName)
        {
            _Hero = superheroService.GetAvenger(superheroName);
        }

        Hero _Hero;

        public Hero AvengerModel
        {
            get { return _Hero; }
        }
    }
}
