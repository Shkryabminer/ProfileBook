using Xamarin.Forms;
using ProfileBook.ViewModels;

namespace ProfileBook.Views
{
    public partial class SignInView : ContentPage
    {
       const double btnSignInheight = 40;
        public SignInView()
        {
            InitializeComponent();
            Button btn = FindByName("btnSignIn") as Button;
            if (btn != null)
            {
                btn.HeightRequest = btnSignInheight;
                btn.CornerRadius = (int)btnSignInheight / 2;
            }
            Label lblSignUp = FindByName("lblSignUp") as Label;
            //if (lblSignUp != null)
            //{
            //    TapGestureRecognizer lblTapGR = new TapGestureRecognizer();
            //    if(this.BindingContext==null)
            //    lblTapGR.Tapped += async (s, e) => { await ((SignInViewViewModel)this.BindingContext).ToSignUpPage(); };
            //    lblSignUp.GestureRecognizers.Add(lblTapGR);
            //}       
             
        }
    }
}
