using Prism;
using Prism.Unity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Ioc;
using ProfileBook.ViewModels;
using ProfileBook.Views;
using ProfileBook.Models;
using ProfileBook.Services;
using ProfileBook.Services.Validators;
using ProfileBook.Services.Autorization;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.Threading.Tasks;
using Acr.UserDialogs;

namespace ProfileBook
{
    public partial class App : PrismApplication
    {
        private IAutorization autorization;
        private IAutorization AutorizationService
        {
            get => autorization ?? (autorization = Container.Resolve<IAutorization>());
        }

        public App()
        {
            InitializeComponent();
        }
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
            // _autorizationService = new AutorizationService();
        }
        protected override async void OnInitialized()
        {
            InitializeComponent();

            await InitNavigationAsync();

        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInView, SignInViewViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SignUpViewViewModel>();
            containerRegistry.RegisterForNavigation<MainListView, MainListViewViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfileBook, AddEditProfileBookViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewViewModel>();

            containerRegistry.RegisterInstance<ISettings>(CrossSettings.Current);
            containerRegistry.RegisterInstance<IUserDialogs>(UserDialogs.Instance);

            //containerRegistry.Register<IItem>();
            // containerRegistry.Register<IUser, User>();
            //  containerRegistry.Register<IProfile, Profile>();

            // containerRegistry.Register<IRepository<User>, _repository<User>>();
            //  containerRegistry.Register<IRepository<Profile>, _repository<Profile>>();
            //containerRegistry.RegisterInstance<IRepository<User>>(Container.Resolve<_repository<User>>());
            //  containerRegistry.RegisterInstance<IRepository<Profile>>(Container.Resolve<_repository<Profile>>());

            containerRegistry.Register<IRepository, Repository>();

            containerRegistry.Register<IAutorization, AutorizationService>();

            containerRegistry.Register<IAuthentificationService, AutentificationService>();
            containerRegistry.Register<IPasswordValidator, PasswordValidator>();
        }
        #region --Private helpers--
        private async Task InitNavigationAsync()
        {
            if (AutorizationService.Autorizeted())
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainListView)}");
            else { await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInView)}"); }
        }
        #endregion
    }
}
