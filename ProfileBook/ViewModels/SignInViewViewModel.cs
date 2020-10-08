using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ProfileBook.Views;
using ProfileBook.Services;
using ProfileBook.Models;

namespace ProfileBook.ViewModels
{
    public class SignInViewViewModel : BaseViewModel
    {
        #region Props
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

        private ICommand _toSignUpViewCommand;
        public ICommand ToSignUpViewCommand => _toSignUpViewCommand ?? (_toSignUpViewCommand = new Command(ToSignUpPage));
        private ICommand _toMainListViewCommand;
        public ICommand ToMainListView => _toMainListViewCommand ?? (_toMainListViewCommand = new Command(SwapToMainView));
        public IAuthentificationService _AuthentificationService { get; private set; }
        #endregion
        public SignInViewViewModel(INavigationService navigationServcie, IAuthentificationService authentificationService) : base(navigationServcie)
        {
            _AuthentificationService = authentificationService;
        }
        public async void ToSignUpPage(object obj)
        {
            await NavigationService.NavigateAsync($"{nameof(SignUpView)}");
        }
        public async void SwapToMainView()
        {
            IUser authUser= _AuthentificationService.GetAuthUser(Login, Password);
            var navParams = new NavigationParameters();
            navParams.Add("User", authUser);
            await NavigationService.NavigateAsync($"{nameof(MainListView)}", navParams) ;
        }
    }
}
