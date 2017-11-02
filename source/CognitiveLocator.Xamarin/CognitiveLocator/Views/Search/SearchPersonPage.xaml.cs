using CognitiveLocator.ViewModels;
using Microsoft.Azure.Mobile.Analytics;
using Xamarin.Forms;

namespace CognitiveLocator.Views
{
    public partial class SearchPersonPage : BasePage
    {
        private int NameRestrictCount = 50;
        private int LastNameRestrictCount = 50;

        public SearchPersonPage()
        {
            InitializeComponent();
        }

        public SearchPersonPage(string type)
        {
            InitializeComponent();
            BindingContext = new SearchPersonViewModel() { SearchType = type };
            ChangeType(type);

            this.FindByName<Entry>("name").TextChanged += NameOnTextChanged;
            this.FindByName<Entry>("lastname").TextChanged += LastNameOnTextChanged;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Analytics.TrackEvent("View: Search Person");
        }

        private void ChangeType(string type)
        {
            if (type.Contains("picture"))
                table.Root.Remove(SectionByName);
            else
                table.Root.Remove(SectionByPicture);
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

        private void OnTextChanged(string entryName, string text, int restrictCount)
        {
            if (text.Length > restrictCount)
            {
                text = text.Remove(text.Length - 1);
                this.FindByName<Entry>(entryName).Text = text;
            }
        }
    }
}