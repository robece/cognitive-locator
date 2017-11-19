using CognitiveLocator.ViewModels;
using Microsoft.AppCenter.Analytics;
using Xamarin.Forms;

namespace CognitiveLocator.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new LoginViewModel();
            Analytics.TrackEvent("View: Login");
        }
    }
}
