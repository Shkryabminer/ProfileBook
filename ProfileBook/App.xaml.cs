﻿using Prism;
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
            containerRegistry.Register<IUser, User>();
            containerRegistry.Register<IProfile, Profile>();
            containerRegistry.Register<IProfileRepository, ProfileRepository>();
            containerRegistry.Register<IUserRepository, UserRepository>();
            containerRegistry.Register<IAuthentificationService, AutentificationService>();
            containerRegistry.Register<IPasswordValidator, PasswordValidator>();
            containerRegistry.RegisterForNavigation<SignInView, SignInViewViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SignUpViewViewModel>();
            containerRegistry.RegisterForNavigation<MainListView, MainListViewViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfileBook, AddEditProfileBookViewModel>();

        }
    }
}
