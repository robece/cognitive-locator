namespace CognitiveLocator.Views
{
    public partial class ReportConfirmationView : ContentPage
    {
        public ReportConfirmationView()
        {
            InitializeComponent();
            Analytics.TrackEvent("View: Report Confirmation");
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}