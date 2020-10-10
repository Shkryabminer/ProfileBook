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
using Acr.UserDialogs;

namespace ProfileBook.ViewModels
{
    public class SignInViewViewModel : BaseViewModel
    {
        #region Props
        string _login;
        bool _signInIsActive;
      //  public event PropertyChangingEventHandler PropertyChanged;
        public bool SignInIsActiv
        {
            get { return !(Login == "" || Password == ""); }
            set
            {
                SetProperty(ref _signInIsActive, value);
               
            }
        }
       //protected override void OnPropertyChanged(string prop="")
       // {
       //     PropertyChanged(this, new PropertyChangingEventArgs(prop));
       // }
        public string Login
        {
            get {
                if (_login == null)
                    _login = ""; 
                return  _login; }
            set { SetProperty(ref _login, value);
                RaisePropertyChanged("SignInIsActiv");
            }
        }

        string _password;
        public string Password
        {
            get
            {
                if (_password == null)
                    _password = "";
                return _password; }
            set { SetProperty(ref _password, value);
                RaisePropertyChanged("SignInIsActiv");
            }
        }

        private ICommand _toSignUpViewCommand;
        public ICommand ToSignUpViewCommand => _toSignUpViewCommand ?? (_toSignUpViewCommand = new Command(ToSignUpPage));
        private ICommand _toMainListViewCommand;
        public ICommand ToMainListViewCommand => _toMainListViewCommand ?? (_toMainListViewCommand = new Command(SwapToMainView));
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
            if (authUser != null)
            {
                var navParams = new NavigationParameters();
                navParams.Add("User", authUser);
                await NavigationService.NavigateAsync($"{nameof(MainListView)}", navParams);
            }
            else
            {
                //ActionSheetConfig config = new ActionSheetConfig();
                //config.Add("Неверный пользователь или пароль", () => Password = "", null);
                //UserDialogs.Instance.ActionSheet(config);
               
              UserDialogs.Instance.Alert("Неверный пользователь или пароль");
                Password = "";

            }
        }
    }
}
