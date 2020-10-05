using Prism;
using Prism.Unity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Ioc;

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
        
        }
        

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
           
        }
    }
}
