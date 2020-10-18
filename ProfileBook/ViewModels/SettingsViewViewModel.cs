using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProfileBook.ViewModels
{
    public class SettingsViewViewModel : BaseViewModel
    {
        #region Props
        bool _sort1, _sort2, _sort3;
        public bool Sort1 { get { return _sort1; }
            set { SetProperty(ref _sort1, value); }
        }
        public bool Sort2 { get { return _sort2; }
            set { SetProperty(ref _sort2, value); } }
        public bool Sort3
        {
            get { return _sort3; }
            set { SetProperty(ref _sort3, value); }
        }

        #endregion
        public SettingsViewViewModel(INavigationService navigation):base(navigation)
        {

        }

    }
}
