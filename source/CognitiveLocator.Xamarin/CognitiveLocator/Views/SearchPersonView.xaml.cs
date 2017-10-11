using System;
using System.Collections.Generic;
using CognitiveLocator.ViewModels;
using Microsoft.Azure.Mobile.Analytics;
using Xamarin.Forms;

namespace CognitiveLocator.Views
{
    public partial class SearchPersonView : BaseView
    {
        private int NameRestrictCount = 50;
        private int LastNameRestrictCount = 50;

        public SearchPersonView()
        {
            InitializeComponent();
        }

        public SearchPersonView(string type)
        {
            InitializeComponent();
            BindingContext = new SearchPersonViewModel(){ SearchType = type };
            ChangeType(type);
            Analytics.TrackEvent("View: Search Person");

            this.FindByName<Entry>("name").TextChanged += NameOnTextChanged;
            this.FindByName<Entry>("lastname").TextChanged += LastNameOnTextChanged;
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