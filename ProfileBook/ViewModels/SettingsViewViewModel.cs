using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using ProfileBook.Translate;
using ProfileBook.Resources;
using System.Runtime.CompilerServices;
using ProfileBook.Themes;
using ProfileBook.Views;
using System.Windows.Input;

namespace ProfileBook.ViewModels
{
    public class SettingsViewViewModel : BaseViewModel
    {

        private bool _sort1, _sort2, _sort3;

        private bool _enabledLanguageRus, _enabledLanguageEn;

        private readonly ISettingsManager _settingsManager;

        #region --Public properties--
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
        public bool EnabledLanguageRus
        {
            get { return _enabledLanguageRus; }
            set
            {
                SetProperty(ref _enabledLanguageRus, value);
            }
        }
        public bool EnabledLanguageEN
        {
            get { return _enabledLanguageEn; }
            set
            {
                SetProperty(ref _enabledLanguageEn, value);
            }
        }
        private bool _isSwitherOn;
        public bool IsSwitherOn
        {
            get
            {
                return _settingsManager.ThemaSource == (nameof(Themes.DarkThema));
            }
            set
            {
                SetProperty(ref _isSwitherOn, value);
                OnPropertyChanged(nameof(IsSwitherOn));
            }

        }
        public ICommand TranslateCommand => new Command(OnTranslateCommand);


        #endregion

        public SettingsViewViewModel(INavigationService navigation, ISettingsManager manager) : base(navigation)
        {
            _settingsManager = manager;
            SetSavedSortOptions();
            SetSavedLanguageOption();
        }


        #region --OnCommandHandlers--
        private async void OnTranslateCommand(object obj)
        {
            SetLanguage();
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainListView)}/{nameof(SettingsView)}");
        }

        #endregion

        #region --Overrides--
        protected override async void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsSwitherOn))
            {
                SetTheme();
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
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
            if (_settingsManager.LanguageSource == Constants._english)
            {
                EnabledLanguageEN = true;
                EnabledLanguageRus = false;
            }
            else
            {
                EnabledLanguageRus = true;
                EnabledLanguageEN = false;
            }
        }
        private void SetLanguage()
        {
            if (EnabledLanguageEN)
            {
                EnabledLanguageEN = false;
                EnabledLanguageRus = true;
                _settingsManager.LanguageSource = Constants._russian;
                Resource.Culture = new System.Globalization.CultureInfo(_settingsManager.LanguageSource);
            }
            else
            {
                EnabledLanguageRus = false;
                EnabledLanguageEN = true;
                _settingsManager.LanguageSource = Constants._english;
                Resource.Culture = new System.Globalization.CultureInfo(_settingsManager.LanguageSource);
            }
        }
        private void SetTheme()
        {
            ICollection<ResourceDictionary> mergeDictionaries = Application.Current.Resources.MergedDictionaries;
            mergeDictionaries.Clear();
            if (_isSwitherOn)
            {
                _settingsManager.ThemaSource = (nameof(Themes.DarkThema).ToString());
                mergeDictionaries.Add(new DarkThema());
            }
            else
            {
                _settingsManager.ThemaSource = (nameof(Themes.LightThema));
                mergeDictionaries.Add(new LightThema());
            }
        }
        #endregion

    }
}
