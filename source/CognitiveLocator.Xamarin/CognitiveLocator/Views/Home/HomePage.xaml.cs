using CognitiveLocator.ViewModels;
using Microsoft.Azure.Mobile.Analytics;

namespace CognitiveLocator.Views
{
    public partial class HomePage : BasePage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new HomeViewModel();
            Analytics.TrackEvent("View: Home");
        }
    }
}