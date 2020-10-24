using Prism.Navigation;
using ProfileBook.Services;
using System.Windows.Input;
using Xamarin.Forms;
using ProfileBook.Models;
using ProfileBook.Views;
using ProfileBook.Services.Validators;
using Acr.UserDialogs;
using ProfileBook.Translate;

namespace ProfileBook.ViewModels
{
    public class SignUpViewViewModel : BaseViewModel
    {
        private readonly IPasswordValidator _passwordValidator;
        private readonly IRepository Repository;
        private readonly IUserDialogs _userDialogs;
        private readonly ITRanslate _translator;
     
        #region Public Properties
       
        bool _signInUpActive;
        public bool SignUpIsActiv
        {
            get { return !(Login == "" || Password == ""|| Confirm =="" ||Login==null||Password==null||Confirm==null); }
            set
            {
                SetProperty(ref _signInUpActive, value);
            }
        }

        private string _login;
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

        public SignUpViewViewModel(INavigationService navigationService,
                                   IRepository repository,
                                   IPasswordValidator signUpValidator,
                                   ITRanslate translator,
                                   IUserDialogs userDialogs) 
                                   : base(navigationService)
        {
            _translator = translator;
            Repository = repository;            
            _passwordValidator = signUpValidator;
            _userDialogs = userDialogs;
        }
        #region --OnCommandHandlers--
        public async void AddUser()
        {            
            string message = _passwordValidator.IsValid(Login, Password, Confirm, Repository.GetItems<User>());
           
            if (message == "Valid")
            {
                var navParam = new NavigationParameters();
                navParam.Add("Login", Login);
                Repository.SaveItem(new User(Login, Password));
                await NavigationService.NavigateAsync($"/{nameof(SignInView)}", navParam);
            }
            else
            { 
              string  error = _translator.GetTranslate(message);
                _userDialogs.Alert(error, "Ok");                
            }
        }
        #endregion


    }
}
