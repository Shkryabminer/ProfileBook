using System;
using Xamarin.Forms;

namespace ProfileBook.Views
{
    public partial class MainListView : ContentPage
    {
        public MainListView()
        {try
            {
                InitializeComponent();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        private void ProfilesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
        protected override void OnAppearing()
        {
            //InitializeComponent();
            base.OnAppearing();
            if (BindingContext is IAppearAware appear)
                appear.OnAppearing();
         

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (BindingContext is IAppearAware appear)
                appear.OnDisAppearing();
        }
    }
}
