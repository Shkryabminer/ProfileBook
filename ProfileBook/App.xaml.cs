using Prism;
using Prism.Unity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Ioc;
using ProfileBook.ViewModels;
using ProfileBook.Views;

namespace ProfileBook
{
    public partial class App : PrismApplication
    {
        public App()
        {
            InitializeComponent();
           
        }
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {

        }
       protected override async void OnInitialized()
        {
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInView)}");
        }
        

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInView, SignInViewViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SignUpViewViewModel>();
        }
    }
}
