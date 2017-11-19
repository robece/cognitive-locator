using Microsoft.AppCenter.Analytics;
using Xamarin.Forms;

namespace CognitiveLocator.Pages
{
    public partial class CreateReportPage : BasePage
    {
        private int ReportedByRestrictCount = 200;
        private int NameRestrictCount = 100;
        private int LastNameRestrictCount = 100;
        private int LocationOfLossRestrictCount = 350;
        private int DateOfLossRestrictCount = 100;
        private int ReportIdRestrictCount = 100;

        public CreateReportPage()
        {
            InitializeComponent();

            this.FindByName<Entry>("reportedBy").TextChanged += ReportedByOnTextChanged;
            this.FindByName<Entry>("name").TextChanged += NameOnTextChanged;
            this.FindByName<Entry>("lastname").TextChanged += LastNameOnTextChanged;
            this.FindByName<Entry>("locationOfLoss").TextChanged += LocationOfLossOnTextChanged;
            this.FindByName<Entry>("dateOfLoss").TextChanged += DateOfLossOnTextChanged;
            this.FindByName<Entry>("reportId").TextChanged += ReportIdOnTextChanged;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Analytics.TrackEvent("View: Create Report");
        }

        private void ReportedByOnTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            OnTextChanged("reportedBy", entry.Text, ReportedByRestrictCount);
        }

        private void NameOnTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            OnTextChanged("name", entry.Text, NameRestrictCount);
        }

        private void LastNameOnTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            OnTextChanged("lastname", entry.Text, LastNameRestrictCount);
        }

        private void LocationOfLossOnTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            OnTextChanged("locationOfLoss", entry.Text, LocationOfLossRestrictCount);
        }

        private void DateOfLossOnTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            OnTextChanged("dateOfLoss", entry.Text, DateOfLossRestrictCount);
        }

        private void ReportIdOnTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            OnTextChanged("reportId", entry.Text, ReportIdRestrictCount);
        }

        private void OnTextChanged(string entryName, string text, int restrictCount)
        {
            if ((text.Length > restrictCount) || (text.Contains(".")))
            {
                text = text.Remove(text.Length - 1);
                this.FindByName<Entry>(entryName).Text = text;
            }
        }
    }
}