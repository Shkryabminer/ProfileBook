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
        public List<Profile> Profiles { get; private set; }
        ICommand _selectProfileCommand;
        public ICommand SelectProfileCommand => _selectProfileCommand ?? (_selectProfileCommand = new Command(SelectProfile));



        ICommand _addNewProfileCommand;
        public ICommand AddNewProfileCommand => _addNewProfileCommand ?? (_addNewProfileCommand = new Command(AddNewProfile));

        private Profile _profile;

        public Profile SelectedProfile
        {
            get
            {
                //if (_profile == null)
                //    _profile = new Profile();
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
        public MainListViewViewModel(INavigationService navigationService, IProfileRepository profilesRepository) : base(navigationService)
        {
            ProfilesRepository = profilesRepository;
        }

        private void OnTappedItem(object sender, ItemTappedEventArgs e)
        {
            if (SelectProfileCommand != null)
            {
                SelectProfileCommand.Execute(e.Item);
            }
        }
       
        public async void AddNewProfile(object obj)
        {
            IProfile prof = new Profile(authUser.UserID);
            SwapToProfilePage(prof);
        }

        private void SelectProfile(object obj)
        {
            SwapToProfilePage(SelectedProfile);
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            //Profiles = ProfilesRepository.GetUserContacts(authUser.UserID).ToList();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
           if(authUser==null)
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

