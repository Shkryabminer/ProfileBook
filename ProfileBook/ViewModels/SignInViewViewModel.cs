using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;
using ProfileBook.Views;
using ProfileBook.Services;
using ProfileBook.Models;
using Acr.UserDialogs;
using ProfileBook.Services.Autorization;

namespace ProfileBook.ViewModels
{
    public class SignInViewViewModel : BaseViewModel
    {
        private readonly IAuthentificationService _authentificationService;

        private readonly IAutorization _autorizator;

        private readonly IUserDialogs _userDialogs;
        
        #region --Public Properties--       
       
        bool _signInIsActive;

        public bool SignInIsActiv
        {
            get { return !(Login == "" || Password == ""); }
            set
            {
                SetProperty(ref _signInIsActive, value);
            }
        }

        private double _btnLoginHeight=40;
        public double BtnLoginHeight
        {
            get => _btnLoginHeight;
            set => SetProperty(ref _btnLoginHeight, value);
        }

        private int _btnLoginCornerRadiuses;
        public int BtnLoginCornerRadiuses
        {
            get => (int)BtnLoginHeight / 2;
            set => SetProperty(ref _btnLoginCornerRadiuses, value);
        }
        string _login;
        public string Login
        {
            get
            {
                if (_login == null)
                    _login = "";
                return _login; }
            set 
            { 
                SetProperty(ref _login, value);
                RaisePropertyChanged("SignInIsActiv");
            }
        }
       private string _password;
        public string Password
        {
            get
            {
                if (_password == null)
                    _password = "";
                return _password; }
            set
            { 
                SetProperty(ref _password, value);
                RaisePropertyChanged("SignInIsActiv");
            }
        }
        #endregion

        #region --Commands--
        private ICommand _toSignUpViewCommand;
        public ICommand ToSignUpViewCommand => _toSignUpViewCommand ?? (_toSignUpViewCommand = new Command(ToSignUpPage));
        private ICommand _toMainListViewCommand;
        public ICommand ToMainListViewCommand => _toMainListViewCommand ?? (_toMainListViewCommand = new Command(SwapToMainView));
        #endregion

        public SignInViewViewModel(INavigationService navigationServcie,
                                   IAuthentificationService authentificationService,
                                   IAutorization autorizator,
                                   IUserDialogs userDialogs)
                                   : base(navigationServcie)
        {
            _userDialogs = userDialogs;
            _authentificationService = authentificationService;
            _autorizator = autorizator;
        }

        #region --OnCommandsHandler--
       
        public async void ToSignUpPage(object obj)
        {
            await NavigationService.NavigateAsync($"{nameof(SignUpView)}");
        }

        public async void SwapToMainView()
        {
            
            IUser authUser= _authentificationService.GetAuthUser(Login, Password);
            if (authUser != null)
            {               
                _autorizator.SetActiveUser(authUser.ID);
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainListView)}");
            }
            else
            {               
              _userDialogs.Alert("Неверный пользователь или пароль");
                Password = "";
            }
        }
        #endregion

        #region --Overrides--
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            string recivedLogin;
            parameters.TryGetValue<string>("Login", out recivedLogin);
            if (recivedLogin == null)
                recivedLogin = "";
            Login = recivedLogin;
            Password = "";
        }
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
           
        }
        #endregion
    }
}
