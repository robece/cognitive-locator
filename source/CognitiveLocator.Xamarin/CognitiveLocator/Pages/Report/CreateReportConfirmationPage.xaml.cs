using Microsoft.Azure.Mobile.Analytics;
using CognitiveLocator.ViewModels;
using Xamarin.Forms;

namespace CognitiveLocator.Pages
{
    public partial class CreateReportConfirmationPage : BasePage
    {
        public CreateReportConfirmationPage()
        {
            InitializeComponent();
            BindingContext = new CreateReportConfirmationViewModel();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Analytics.TrackEvent("View: Report Confirmation");
        }
    }
}