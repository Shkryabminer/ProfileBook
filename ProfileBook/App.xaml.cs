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

namespace ProfileBook
{
    public partial class App : PrismApplication
    {
       IAutorization Autorizator { get; set; }
        public App()
        {
            InitializeComponent();

        }
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
           // Autorizator = new AutorizationService();
        }
        protected override async void OnInitialized()
        {
            Autorizator = new AutorizationService();
            //await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInView)}");
            if ((Autorizator.Autorizeted()))
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainListView)}");
            else { await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInView)}"); }
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInView, SignInViewViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SignUpViewViewModel>();
            containerRegistry.RegisterForNavigation<MainListView, MainListViewViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfileBook, AddEditProfileBookViewModel>();

            containerRegistry.Register<IUser, User>();
            containerRegistry.Register<IProfile, Profile>();

            containerRegistry.Register<IAutorization,AutorizationService>();
            containerRegistry.Register<IProfileRepository, ProfileRepository>();            
            containerRegistry.Register<IUserRepository, UserRepository>();
            containerRegistry.Register<IAuthentificationService, AutentificationService>();
            containerRegistry.Register<IPasswordValidator, PasswordValidator>();
            

        }
    }
}
