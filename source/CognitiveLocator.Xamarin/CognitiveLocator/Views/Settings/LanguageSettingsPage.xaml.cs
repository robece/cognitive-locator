using CognitiveLocator.ViewModels;
using Microsoft.Azure.Mobile.Analytics;
using Xamarin.Forms;

namespace CognitiveLocator.Views
{
    public partial class LanguageSettingsPage : BasePage
    {
        public LanguageSettingsPage()
        {
            InitializeComponent();
            BindingContext = new LanguageSettingsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Analytics.TrackEvent("View: Language Settings");
        }
    }
}
