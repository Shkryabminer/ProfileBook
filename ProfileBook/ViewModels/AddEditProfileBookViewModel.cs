using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class AddEditProfileBookViewModel : BaseViewModel
    {
        #region Props

        IProfile _profile;
        bool _saveIsActive;
        public bool SaveIsActive
        {
            get { return !(_Profile.FirstName == "" || _Profile.FirstName == null || _Profile.SecondName == "" || _Profile.SecondName == null); }
            set { SetProperty(ref _saveIsActive, value); }
        }
        public IProfile _Profile
        {
            set { SetProperty(ref _profile, value);
                RaisePropertyChanged("SaveIsActive");
            }
            get { return _profile; }
        }
       ICommand _saveProfileCommand;
        public ICommand SaveProfileCommand => _saveProfileCommand ?? (_saveProfileCommand = new Command(SaveProfile));

        #endregion
        public IProfileRepository ProfilesRepository { get; private set; }
        public AddEditProfileBookViewModel(INavigationService navigationServcie, IProfileRepository profilesRepository) : base(navigationServcie)
        {
            ProfilesRepository = profilesRepository;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            _Profile = parameters.GetValue<IProfile>("Profile") as Profile;
            base.OnNavigatedTo(parameters);
        }
         void SaveProfile()
        {
            ProfilesRepository.SaveContact(_Profile);
        }




    }
}
