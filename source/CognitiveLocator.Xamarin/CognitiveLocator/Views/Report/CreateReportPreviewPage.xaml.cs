using CognitiveLocator.ViewModels;
using Microsoft.Azure.Mobile.Analytics;

namespace CognitiveLocator.Views
{
    public partial class CreateReportPreviewPage : BasePage
    {
        public CreateReportPreviewPage()
        {
            InitializeComponent();
        }

        public CreateReportPreviewPage(BaseViewModel reportContext)
        {
            InitializeComponent();
            this.BindingContext = reportContext;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Analytics.TrackEvent("View: Preview Page");
        }
    }
}