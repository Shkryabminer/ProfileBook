using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using ProfileBook.Translate;

namespace ProfileBook.ViewModels
{
    public class SettingsViewViewModel : BaseViewModel
    {
        #region --Properties--
        private bool _sort1, _sort2, _sort3;
        private bool _enabledLanguageRus, _enabledLanguageEn;
        public bool Sort1
        {
            get { return _sort1; }
            set
            {
                SetProperty(ref _sort1, value);
                if (Sort1)
                {
                    Sort2 = false; Sort3 = false;
                    SetSortOption();
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
                    SetSortOption();
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
                    SetSortOption();
                }
            }
        }
        public bool EnabledLanguageRus {
            get { return _enabledLanguageRus; }
            set {
                
                SetProperty(ref _enabledLanguageRus, value);
                if (EnabledLanguageRus)
                {
                    EnabledLanguageEN = false;
                    _settingsManager.LanguageSource = Constants._russian;
                    
                }
            }
        }
        public bool EnabledLanguageEN
        {
            get { return _enabledLanguageEn; }
            set {               
                SetProperty(ref _enabledLanguageEn, value);
                if (EnabledLanguageEN)
                {
                    EnabledLanguageRus = false;
                    _settingsManager.LanguageSource = Constants._defaultlanguage;
                    
                }
            }
        }

        private readonly ISettingsManager _settingsManager;

        #endregion
        public SettingsViewViewModel(INavigationService navigation, ISettingsManager manager) : base(navigation)
        {
            _settingsManager = manager;
            SetSavedSortOptions();
            SetSavedLanguageOption();
        }

        #region --Overrides--
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            SetSavedLanguageOption();
        }
        #endregion

        #region --Privat helpers--
        private void SetSortOption()
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
        private void SetSavedSortOptions()
        {
            if (_settingsManager.SelectedSortMethode == 0)
                Sort1 = true;
            else if (_settingsManager.SelectedSortMethode == 1)
                Sort2 = true;
            else if (_settingsManager.SelectedSortMethode == 2)
                Sort3 = true;
        }
        
        private void SetSavedLanguageOption()
        {
            if (_settingsManager.LanguageSource == Constants._defaultlanguage)
                EnabledLanguageEN = true;
            else EnabledLanguageRus = true;
        }
        #endregion

    }
}
