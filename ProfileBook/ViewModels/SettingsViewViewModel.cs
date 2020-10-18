using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProfileBook.ViewModels
{
    public class SettingsViewViewModel : BaseViewModel
    {
        #region --Properties--
       private bool _sort1, _sort2, _sort3;
        public bool Sort1 { get { return _sort1; }
            set { SetProperty(ref _sort1, value); SetOption(); }
        }
        public bool Sort2 { get { return _sort2; }
            set { SetProperty(ref _sort2, value); SetOption(); } }
        public bool Sort3
        {
            get { return _sort3; }
            set { SetProperty(ref _sort3, value); SetOption(); }
        }
        private readonly ISettingsManager _settingsManager;

        #endregion
        public SettingsViewViewModel(INavigationService navigation, ISettingsManager manager):base(navigation)
        {
            _settingsManager = manager;
        }
        #region --Privat helpers--
        private void SetOption()
        {
            if (Sort1)
                _settingsManager.SelectedSortMethode = 0;
            else if (Sort2)
                _settingsManager.SelectedSortMethode = 1;
            else if (Sort3)
                _settingsManager.SelectedSortMethode = 2;
        }
        #endregion

    }
}
