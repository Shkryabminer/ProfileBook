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

namespace ProfileBook.ViewModels
{
    public class MainListViewViewModel : BaseViewModel
    {
        #region Props
        IUser authUser;
        public IAutorization Autorizator { get; private set; }
        public IRepository Repository { get; private set; }
        //public IProfileRepository ProfilesRepository { get; private set; } Repository
        private List<Profile> _profiles;
        public List<Profile> Profiles
        {
            get { return _profiles; }
            set
            {
                SetProperty(ref _profiles, value);
                RaisePropertyChanged("LabelIsVisible");
            }
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

              //  OnPropertyChanged("SelectedProfile");
                    

            }
        }
        //protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    if (propertyName == "SelectedProfile"&&SelectedProfile!=null)
        //    { ShowImageCommand.Execute(SelectedProfile); }
        //    base.OnPropertyChanged(propertyName);
        //}
        bool _labelIsVisible;
        public bool LabelIsVisible
        {
            get { return (Profiles != null && Profiles.Count == 0); }
            set
            { SetProperty(ref _labelIsVisible, value); }
        }
        
        #endregion
        #region Commands
        ICommand _selectProfileCommand;
        public ICommand SelectProfileCommand => _selectProfileCommand ?? (_selectProfileCommand = new Command<object>(SelectProfile));

        ICommand _addNewProfileCommand;
        public ICommand AddNewProfileCommand => _addNewProfileCommand ?? (_addNewProfileCommand = new Command(AddNewProfile));
        ICommand _deleteProfileCommand;
        public ICommand DeleteProfileCommand => _deleteProfileCommand ?? (_deleteProfileCommand = new Command<object>(DeleteProfile));

        ICommand _logOutCommand;
        public ICommand LogOutCommand => _logOutCommand ?? (_logOutCommand = new Command(LogOut));

        ICommand _showImageCommand;
        public ICommand ShowImageCommand => _showImageCommand ?? (_showImageCommand = new Command<object>(ShowImage));

        private async void ShowImage(object obj)
        {
            var prof = obj as Profile;
            ActionSheetConfig config = new ActionSheetConfig();
            config.SetUseBottomSheet(true);
            var galeryIcon = await BitmapLoader.Current.LoadFromResource(prof.Picture, 150f, 150f);            
            config.Add(null, null, galeryIcon);
            config.SetCancel(null, null, null);
            UserDialogs.Instance.ActionSheet(config);
        }
        #endregion
        //public MainListViewViewModel(INavigationService navigationService, IProfileRepository profilesRepository, IAutorization autorization) : base(navigationService)
        //{
        //    ProfilesRepository = profilesRepository;
        //    Autorizator = autorization;
        //}
        public MainListViewViewModel(INavigationService navigationService, IRepository repository,
            IAutorization autorization) : base(navigationService)
        {
            Repository = repository;            
            Autorizator = autorization;
        }

        public void AddNewProfile(object obj)
        {
            //IProfile prof = new Profile(authUser.ID);
            IProfile prof = new Profile(Autorizator.GetActiveUser());
            SwapToProfilePage(prof);
        }

        private void SelectProfile(object obj)
        {
            SwapToProfilePage(obj as Profile);
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            //Profiles = ProfilesRepository.GetUserContacts(authUser.ID).ToList();
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            //if (!Autorizator.Autorizeted())
            //{
            //    await NavigationService.NavigateAsync($"/{nameof(SignInView)}");
            //}
            // else
            //{               
            //if (authUser == null)//проверяется был ли установлен активный пользователь ранее
            //    authUser = parameters.GetValue<User>("User") as User;
            //Profiles = ProfilesRepository.GetUserContacts(authUser.ID).ToList();
            if (Autorizator.Autorizeted())
            {
                var query = from p in Repository.GetItems<Profile>()
                            where p.UserID == Autorizator.GetActiveUser()
                            select p;
                Profiles = query.ToList();
                // Profiles = ProfilesRepository.GetUserContacts(Autorizator.GetActiveUser()).ToList(); }
            }
            //}
        }

        private async void SwapToProfilePage(IProfile profile)
        {
            var navParam = new NavigationParameters();
            navParam.Add("Profile", profile);
            await NavigationService.NavigateAsync($"{nameof(AddEditProfileBook)}", navParam);
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
                        Repository.DeleteItem<Profile>(item);
                        //Repository.DeleteContact(item.ID);
                        //Profiles = ProfilesRepository.GetUserContacts(authUser.ID).ToList();
                        var query = from p in Repository.GetItems<Profile>()
                                    where p.UserID == Autorizator.GetActiveUser()
                                    select p;
                        Profiles = query.ToList();
                    }
                });
                UserDialogs.Instance.Confirm(config);
            }

        }
        private async void LogOut(object obj)
        {
            Autorizator.LogOut();
            //  await NavigationService.GoBackToRootAsync();
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInView)}");
        }

        public void ShowImage(object sender, SelectedItemChangedEventArgs e)
        { 
        
        }
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            Console.WriteLine("Overrided Inintialize mainlist");
        }


    }
}

