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

namespace ProfileBook.ViewModels
{
    public class MainListViewViewModel : BaseViewModel
    {
        #region Props
        IUser authUser;
        public IProfileRepository ProfilesRepository { get; private set; }
        private List<Profile> _profiles;
        public List<Profile> Profiles
        {
            get { return _profiles; }
            set { SetProperty(ref _profiles, value); }
        }
       

        private Profile _profile;

        public Profile SelectedProfile
        {
            get
            {              
                return _profile;
            }
            set
            {
                SetProperty(ref _profile, value);

                if (_profile != null)
                    SelectProfileCommand.Execute(_profile);
            }
        }
        #endregion
        #region Commands
        ICommand _selectProfileCommand;
        public ICommand SelectProfileCommand => _selectProfileCommand ?? (_selectProfileCommand = new Command(SelectProfile));

        ICommand _addNewProfileCommand;
        public ICommand AddNewProfileCommand => _addNewProfileCommand ?? (_addNewProfileCommand = new Command(AddNewProfile));
        #endregion
        public MainListViewViewModel(INavigationService navigationService, IProfileRepository profilesRepository) : base(navigationService)
        {
            ProfilesRepository = profilesRepository;            
        }       
       
        public  void AddNewProfile(object obj)
        {
            IProfile prof = new Profile(authUser.UserID);
            SwapToProfilePage(prof);
        }

        private void SelectProfile(object obj)
        {
            SwapToProfilePage(SelectedProfile);
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            //Profiles = ProfilesRepository.GetUserContacts(authUser.UserID).ToList();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (authUser == null)//проверяется был ли установлен активный пользователь ранее
                authUser = parameters.GetValue<User>("User") as User;
            Profiles = ProfilesRepository.GetUserContacts(authUser.UserID).ToList();
        }
       
        private async void SwapToProfilePage(IProfile profile)
        {
            var navParam = new NavigationParameters();            
            navParam.Add("Profile", profile);
            await NavigationService.NavigateAsync($"{nameof(AddEditProfileBook)}", navParam);
        }
    }
}

