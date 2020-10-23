using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services;
using ProfileBook.Services.ProfileService;
using ProfileBook.Translate;
using Splat;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProfileBook.ViewModels
{
    public class AddEditProfileBookViewModel : BaseViewModel
    {
        private ITRanslate _allertErrorTranslator;
        private readonly IMedia _mediaPlugin;
        private readonly IUserDialogs _userDialogs;
        private readonly IProfileService _profileService;
        private Profile _currentProfile;

        #region --Public Properties--
        bool _saveIsActive;
        public bool SaveIsActive
        {
            get {
                return ((CurrentProfile.FirstName != "" && CurrentProfile.FirstName != null)
                    && (CurrentProfile.SecondName != "" && CurrentProfile.SecondName != null)); 
            }
            set { SetProperty(ref _saveIsActive, value); }
        }
        public Profile CurrentProfile
        {
            set {
                SetProperty(ref _currentProfile, value);
                
                RaisePropertyChanged(nameof(SaveIsActive));
                }
            get { return _currentProfile; }
        }
        #endregion
      
        #region Commands
        ICommand _saveProfileCommand;
        public ICommand SaveProfileCommand => _saveProfileCommand ?? (_saveProfileCommand = new Command(SaveProfile));
        ICommand _imageTapCommand;
        public ICommand ImageTapCommand => _imageTapCommand ?? (_imageTapCommand = new Command(ImageTap));
        #endregion


        public AddEditProfileBookViewModel(
              INavigationService navigationServcie,
             ITRanslate extension,
             IProfileService profileService,
             IUserDialogs userDialogs,
             IMedia media) :base(navigationServcie)
        {
            _allertErrorTranslator = extension;
            _mediaPlugin = media;
            _userDialogs = userDialogs;
            _profileService = profileService;        
        }

        #region --Overrides--
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            CurrentProfile = parameters.GetValue<IProfile>("Profile") as Profile;
            base.OnNavigatedTo(parameters);
        }
        #endregion

        #region --Command handlers--

        private async void SaveProfile()
        {if (CheckFields())
            {
                CurrentProfile.Date = DateTime.Now;
                _profileService.SaveOrUpdateProfile(CurrentProfile);
                await NavigationService.GoBackAsync();
            }    
        else
            {
                await _userDialogs.AlertAsync(GetTranslate());
            }
        }

        private async void  ImageTap(object obj)
        {
            ActionSheetConfig config = new ActionSheetConfig();
            config.SetUseBottomSheet(true);
            var galeryIcon = await BitmapLoader.Current.LoadFromResource("ic_collections.png",500f,500f);
            var photoIcon = await BitmapLoader.Current.LoadFromResource("ic_camera_alt.png", 500f, 500f);
            config.Add("Take Picture From Galery", SetPictureFromGalery, galeryIcon);
            config.Add("Take Picture From Camera", SetFromCamera, photoIcon);
            config.SetCancel(null, null, null); 
            _userDialogs.ActionSheet(config);
        }
        #endregion
      
        #region --Private helpers--
        private async void SetPictureFromGalery()
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                MediaFile file = await _mediaPlugin.PickPhotoAsync();
                CurrentProfile.Picture = file.Path;
                RaisePropertyChanged(nameof(CurrentProfile));
            }
        }
        
        private async void SetFromCamera()
        {
            if (_mediaPlugin.IsTakePhotoSupported && _mediaPlugin.IsCameraAvailable)
            {
                var options = new StoreCameraMediaOptions();
                options.SaveToAlbum = true;
                options.PhotoSize = PhotoSize.Custom;
                options.CustomPhotoSize = 600;

                MediaFile file = await _mediaPlugin.TakePhotoAsync(options);
                if (file == null)
                    return;
                CurrentProfile.Picture = file.Path;
                RaisePropertyChanged(nameof(CurrentProfile));
            }
        }
        private bool CheckFields()
        {
            return (!string.IsNullOrEmpty(CurrentProfile.FirstName) && !string.IsNullOrEmpty(CurrentProfile.SecondName));
        }
        private string GetTranslate()
        {
          return  _allertErrorTranslator.GetTranslate("EditPrifileError");
        }
        #endregion



    }
}
