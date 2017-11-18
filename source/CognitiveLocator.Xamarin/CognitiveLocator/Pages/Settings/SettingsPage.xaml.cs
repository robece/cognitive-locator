using CognitiveLocator.ViewModels;
using Microsoft.Azure.Mobile.Analytics;
using Xamarin.Forms;

namespace CognitiveLocator.Pages
{
    public partial class SettingsPage : BasePage
    {
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = new SettingsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new SettingsViewModel();
            Analytics.TrackEvent("View: Settings");
        }
    }
}
