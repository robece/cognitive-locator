using CognitiveLocator.ViewModels;
using Microsoft.AppCenter.Analytics;
using Xamarin.Forms;

namespace CognitiveLocator.Pages
{
    public partial class HomePage : BasePage
    {
        public HomePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new HomeViewModel();
            Analytics.TrackEvent("View: Home");

            MessagingCenter.Subscribe<object>(this, "connected", (sender) => {
                Device.BeginInvokeOnMainThread(() => {
                    WelcomeUser.Text = Settings.FacebookProfile.name;
                });
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object>(this, "connected");
        }
    }
}