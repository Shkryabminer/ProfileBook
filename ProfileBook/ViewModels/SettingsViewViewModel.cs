using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SettingsViewViewModel : BaseViewModel
    {
        #region --Properties--
        private bool _sort1, _sort2, _sort3;
        public bool Sort1
        {
            get { return _sort1; }
            set
            {
                SetProperty(ref _sort1, value);
                if (Sort1)
                {
                    Sort2 = false; Sort3 = false;

                    SetOption();
                }
            }
        }
        public bool Sort2
        {
            get { return _sort2; }
            set
            {
                SetProperty(ref _sort2, value);
                if (Sort2)
                {
                    Sort1 = false; Sort3 = false;
                    SetOption();
                }

            }
        }
        public bool Sort3
        {
            get { return _sort3; }
            set
            {
                SetProperty(ref _sort3, value);
                if (Sort3)
                {
                    Sort1 = false; Sort2 = false;
                    SetOption();
                }
            }
        }
        private readonly ISettingsManager _settingsManager;

        #endregion
        public SettingsViewViewModel(INavigationService navigation, ISettingsManager manager) : base(navigation)
        {
            _settingsManager = manager;
            SetSavedOptions();

        }
        #region --Privat helpers--
        private void SetOption()
        {
            if (Sort1)
            {
                _settingsManager.SelectedSortMethode = 0;
            }
            else if (Sort2)
            {
                _settingsManager.SelectedSortMethode = 1;

            }
            else if (Sort3)
            {
                _settingsManager.SelectedSortMethode = 2;
            }
            if (!Sort1 && !Sort2 && !Sort3)
                _settingsManager.SelectedSortMethode = 0;
        }
        private void SetSavedOptions()
        {
            if (_settingsManager.SelectedSortMethode == 0)
                Sort1 = true;
            else if (_settingsManager.SelectedSortMethode == 1)
                Sort2 = true;
            else if (_settingsManager.SelectedSortMethode == 2)
                Sort3 = true;
        }
        #endregion

    }
}
