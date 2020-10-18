using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services;
using Splat;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;



namespace ProfileBook.ViewModels
{
    public class AddEditProfileBookViewModel : BaseViewModel
    {
        #region --Properties--

       private Profile _currentProfile;
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


        public IRepository Repository { get; private set; }
        //public IProfileRepository ProfilesRepository { get; private set; } _repository
        //public AddEditProfileBookViewModel(INavigationService navigationServcie, IProfileRepository profilesRepository) : base(navigationServcie)
        //{
        //    ProfilesRepository = profilesRepository;
        //}
        public AddEditProfileBookViewModel(INavigationService navigationServcie, IRepository repository) : base(navigationServcie)
        {
            Repository = repository;
        }


        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            CurrentProfile = parameters.GetValue<IProfile>("Profile") as Models.Profile;
            base.OnNavigatedTo(parameters);
        }
       private  async void SaveProfile()
        {
            CurrentProfile.Date = DateTime.Now;
            Repository.SaveItem(CurrentProfile);
            await NavigationService.GoBackAsync();
        }
        private async void  ImageTap(object obj)
        {
            ActionSheetConfig config = new ActionSheetConfig();
            config.SetUseBottomSheet(true);
            var galeryIcon = await BitmapLoader.Current.LoadFromResource("ic_collections.png",20f,20f);
            var photoIcon = await BitmapLoader.Current.LoadFromResource("ic_camera_alt.png", 20f, 20f);
            config.Add("Take Picture From Galery", SetPictureFromGalery, galeryIcon);
            config.Add("Take Picture From Camera", SetFromCamera, photoIcon);
            config.SetCancel(null, null, null); 
            UserDialogs.Instance.ActionSheet(config);
        }
        private async void SetPictureFromGalery()
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                MediaFile file = await CrossMedia.Current.PickPhotoAsync();
                CurrentProfile.Picture = file.Path;
                RaisePropertyChanged(nameof(CurrentProfile));
            }
        }
       private async void SetFromCamera()
        {
            if (CrossMedia.Current.IsTakePhotoSupported && CrossMedia.Current.IsCameraAvailable)
            {
                var options = new StoreCameraMediaOptions();
                options.SaveToAlbum = true;
                MediaFile file = await CrossMedia.Current.TakePhotoAsync(options);
                if (file == null)
                    return;
                CurrentProfile.Picture = file.Path;
                RaisePropertyChanged(nameof(CurrentProfile));
            }
        }
        


    }
}
