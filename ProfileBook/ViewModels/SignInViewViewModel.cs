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

namespace ProfileBook.ViewModels
{
    public class SignInViewViewModel : BaseViewModel
    {
        private ICommand _toSignUpViewCommand;
        public ICommand ToSignUpViewCommand => _toSignUpViewCommand ?? (_toSignUpViewCommand = new Command(ToSignUpPage));

        public SignInViewViewModel(INavigationService navigationServcie):base(navigationServcie)
        {

        }
        public async void ToSignUpPage(object obj)
        {
            await NavigationService.NavigateAsync($"{nameof(SignUpView)}");
        }
    }
}
