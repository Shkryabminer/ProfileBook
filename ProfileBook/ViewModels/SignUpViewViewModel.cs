using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using ProfileBook.Services;
using System.Windows.Input;
using Xamarin.Forms;
using ProfileBook.Models;

namespace ProfileBook.ViewModels
{
    public class SignUpViewViewModel : BaseViewModel
    {
        #region Properties
        string _login;
        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }
        string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { SetProperty(ref _confirmPassword, value); }
        }
        ICommand _createUser;
        public ICommand CreateUser => _createUser ?? (_createUser = new Command(AddUser));
        #endregion

        public IUserRepository UsersRepository { get; private set; }
        public SignUpViewViewModel(INavigationService navigationService, IUserRepository usersRepository) : base(navigationService)
        {
            UsersRepository = usersRepository;
        }
        public async void AddUser()
        {
            UsersRepository.SaveUser(new User(Login, Password));
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}");
        }



    }
}
