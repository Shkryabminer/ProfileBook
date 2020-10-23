using Prism.Mvvm;
using Prism.Navigation;
using System.Threading.Tasks;

namespace ProfileBook
{
    public class BaseViewModel : BindableBase, INavigationAware,IInitialize,IInitializeAsync
    {
        protected  INavigationService NavigationService { get; private set; }
        public BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        #region --Overrides--
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

       
        #endregion
    }
}
