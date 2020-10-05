using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook
{
    public class BaseViewModel : BindableBase, INavigationAware,IInitialize,IInitializeAsync
    {
        public INavigationService NavigationService { get; private set; }
        public BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
           
        }

        public virtual void Initialize(INavigationParameters parameters)
        {
            
        }

        public virtual async Task InitializeAsync(INavigationParameters parameters)
        {
            
        }
    }
}
