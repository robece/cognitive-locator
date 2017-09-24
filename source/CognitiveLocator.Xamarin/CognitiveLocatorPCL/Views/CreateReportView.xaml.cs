using System;
using System.Collections.Generic;
using Microsoft.Azure.Mobile.Analytics;
using Xamarin.Forms;

namespace CognitiveLocator.Views
{
    public partial class CreateReportView : BaseView
    {
        private int NameRestrictCount = 50;
        private int LastNameRestrictCount = 50;
        private int AgeRestrictCount = 2;
        private int LocationRestrictCount = 350;
        private int NotesRestrictCount = 500;
        private int AliasRestrictCount = 500;
        private int ReportedByRestrictCount = 500;

        public CreateReportView()
        {
            InitializeComponent();
            Analytics.TrackEvent("View: Create Report");

            this.FindByName<Entry>("name").TextChanged += NameOnTextChanged;
            this.FindByName<Entry>("lastname").TextChanged += LastNameOnTextChanged;
            this.FindByName<Entry>("age").TextChanged += AgeOnTextChanged;
            this.FindByName<Entry>("location").TextChanged += LocationOnTextChanged;
            this.FindByName<Entry>("notes").TextChanged += NotesOnTextChanged;
            this.FindByName<Entry>("alias").TextChanged += AliasOnTextChanged;
            this.FindByName<Entry>("reportedby").TextChanged += ReportedByOnTextChanged;
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

        private void AgeOnTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            OnTextChanged("age", entry.Text, AgeRestrictCount);
        }

        private void LocationOnTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            OnTextChanged("location", entry.Text, LocationRestrictCount);
        }

        private void NotesOnTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            OnTextChanged("notes", entry.Text, NotesRestrictCount);
        }

        private void AliasOnTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            OnTextChanged("alias", entry.Text, AliasRestrictCount);
        }

        private void ReportedByOnTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            OnTextChanged("reportedby", entry.Text, ReportedByRestrictCount);
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
