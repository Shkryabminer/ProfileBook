using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using ProfileBook.Services;
using System.Windows.Input;
using Xamarin.Forms;
using ProfileBook.Models;
using ProfileBook.Views;
using ProfileBook.Services.Validators;
using Acr.UserDialogs;

namespace ProfileBook.ViewModels
{
    public class SignUpViewViewModel : BaseViewModel
    {
        #region Properties
        string _login;
        bool _signInUpActive;
        
        public bool SignUpIsActiv
        {
            get { return !(Login == "" || Password == ""|| Confirm =="" ||Login==null||Password==null||Confirm==null); }
            set
            {
                SetProperty(ref _signInUpActive, value);
            }
        }
        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value);
                RaisePropertyChanged("SignUpIsActiv");
            }
        }
        string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value);
                RaisePropertyChanged("SignUpIsActiv");
            }
        }
        string _confirm;
        public string Confirm
        {
            get { return _confirm; }
            set { SetProperty(ref _confirm, value);
                RaisePropertyChanged("SignUpIsActiv");
                }
        }
        ICommand _createUser;
        public ICommand CreateUser => _createUser ?? (_createUser = new Command(AddUser));
        #endregion
        public IRepository Repository { get; private set; }
        //public IUserRepository UsersRepository { get; private set; } _repository
        public IPasswordValidator SignUpValidator { get; private set; }
        //public SignUpViewViewModel(INavigationService navigationService, IUserRepository usersRepository, IPasswordValidator signUpValidator) : base(navigationService)
        //{
        //    UsersRepository = usersRepository;//_repository
        //    SignUpValidator = signUpValidator;
        //}
        public SignUpViewViewModel(INavigationService navigationService, IRepository repository, IPasswordValidator signUpValidator) : base(navigationService)
        {
            Repository = repository;
            //UsersRepository = usersRepository;//_repository
            SignUpValidator = signUpValidator;
        }
        public async void AddUser()
        {
            // string message = SignUpValidator.IsValid(Login, Password, Confirm, UsersRepository.GetUsers());_repository
            string message = SignUpValidator.IsValid(Login, Password, Confirm, Repository.GetItems<User>());
            if (message == "Valid")
            {
                var navParam = new NavigationParameters();
                navParam.Add("Login", Login);
                Repository.SaveItem(new User(Login, Password));
                await NavigationService.NavigateAsync($"/{nameof(SignInView)}",navParam);
            }
            else UserDialogs.Instance.Alert(message, "Ok");
        }



    }
}
