using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using ProfileBook.Views;
using ProfileBook.Services;
using Acr.UserDialogs;
using Splat;
using System.Threading;
using System.Runtime.CompilerServices;
using ProfileBook.Services.Autorization;
using ProfileBook.Services.ProfileService;
using System.Threading.Tasks;

namespace ProfileBook.ViewModels
{
    public class MainListViewViewModel : BaseViewModel
    {
        #region --Properties--
     
        private readonly IUserDialogs _userDialogs;
        private readonly IAutorization _autorizationService;        
        private readonly IProfileService _profileService;
        private List<Profile> _profiles;
        public List<Profile> Profiles
        {
            get => _profiles;
            set
            {
                SetProperty(ref _profiles, value);
                RaisePropertyChanged("LabelIsVisible");
            }
        }
        private Profile _selectedProfile;
        public Profile SelectedProfile
        {
            get=>_selectedProfile;
            
            set=>SetProperty(ref _selectedProfile, value);            
        }
        private bool _labelIsVisible;
        public bool LabelIsVisible
        {
            get =>(Profiles != null && Profiles.Count == 0); 
           
            set=> SetProperty(ref _labelIsVisible, value); 
        }
        private string _date;
        public string Date {
            set { SetProperty(ref _date, value); }
            get => SelectedProfile.Date.ToLongTimeString();
        }
        #endregion

        #region Commands
        private ICommand _selectProfileCommand;
        public ICommand SelectProfileCommand => _selectProfileCommand ?? (_selectProfileCommand = new Command<object>(SelectProfile));

        private ICommand _setingsButtonCommand;
        public ICommand SettingsButtonCommand => _setingsButtonCommand ?? (_setingsButtonCommand = new Command(OnSettingsButton));        

        private ICommand _addNewProfileCommand;
        public ICommand AddNewProfileCommand => _addNewProfileCommand ?? (_addNewProfileCommand = new Command(AddNewProfile));
       
        private ICommand _deleteProfileCommand;
        public ICommand DeleteProfileCommand => _deleteProfileCommand ?? (_deleteProfileCommand = new Command<object>(DeleteProfile));

        ICommand _logOutCommand;
        public ICommand LogOutCommand => _logOutCommand ?? (_logOutCommand = new Command(LogOut));

        ICommand _showImageCommand;
        public ICommand ShowImageCommand => _showImageCommand ?? (_showImageCommand = new Command<object>(ShowImage));

        private ICommand _buttonImageCommand;
        public ICommand ButtonImageCommand => _buttonImageCommand ?? (_buttonImageCommand = new Command(OnButtonImage));      

        #endregion
       
        public MainListViewViewModel(
            INavigationService navigationService,            
            IAutorization autorization, 
            IProfileService profileService,
            IUserDialogs userDialogs) : base(navigationService)
        {
            _userDialogs = userDialogs;
            _profileService = profileService;            
            _autorizationService = autorization;
        }
       
        #region --Command handlers--
        private  void SelectProfile(object obj)
        {
            SwapToProfilePage(obj as Profile);
        }

        private async void OnButtonImage(object obj)
        {
            var prof = obj as Profile;
            var image = prof.Picture;
            var galeryIcon = await BitmapLoader.Current.LoadFromResource(image, 300f, 300f);
             _userDialogs.ShowImage(galeryIcon, null,5000);
            
        }

        public  void AddNewProfile(object obj)
        {             
            IProfile prof = new Profile(_autorizationService.GetActiveUser());
            SwapToProfilePage(prof);
        }
        private async void ShowImage(object obj)
        {
            var prof = obj as Profile;
            ActionSheetConfig config = new ActionSheetConfig();
            config.SetUseBottomSheet(true);
            var galeryIcon = await BitmapLoader.Current.LoadFromResource(prof.Picture, 150f, 150f);
            config.Add(null, null, galeryIcon);
            config.SetCancel(null, null, null);
            _userDialogs.ActionSheet(config);
        }

        private void DeleteProfile(object obj)
        {
            var item = (obj as Profile);
            if (item != null)
            {
                var config = new ConfirmConfig();
                config.Message = "Do you realy want to delete the profile";
                config.OkText = "Yes";
                config.CancelText = "No";
                config.SetAction((b) =>
                {
                    if (b)
                    {
                        _profileService.DeleteProfile(item);
                        SetProfiles();
                    }
                });
                _userDialogs.Confirm(config);
            }
        }

        private async void OnSettingsButton(object obj)
        {
            await NavigationService.NavigateAsync($"{nameof(SettingsView)}");
        }

        public void ShowImage(object sender, SelectedItemChangedEventArgs e)
        {
        }

        private async void LogOut(object obj)
        {
            _autorizationService.LogOut();            
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInView)}");
        }

        #endregion

        #region --Oveerides--
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {            
        }

        public override  void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            
            if (_autorizationService.Autorizeted())
            {
                SetProfiles();                
            }
                     
       }
        #endregion

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
        }

        #region --Private helpers--
        private async void SwapToProfilePage(IProfile profile)
        {
            var navParam = new NavigationParameters();
            navParam.Add("Profile", profile);
            await NavigationService.NavigateAsync($"{nameof(AddEditProfileBook)}", navParam);
        }
        private void SetProfiles()
        {
            Profiles= _profileService.GetProfiles(_autorizationService.GetActiveUser());
            RaisePropertyChanged($"{nameof(Profiles)}");
        }
        #endregion
    }
}

