using System;
using System.Collections.Generic;
using CognitiveLocator.ViewModels;
using Microsoft.Azure.Mobile.Analytics;
using Xamarin.Forms;

namespace CognitiveLocator.Views
{
    public partial class SearchPersonView : BaseView
    {
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
        }

        private void ChangeType(string type)
        {
            if (type.Contains("picture"))
                table.Root.Remove(SectionByName);
            else
                table.Root.Remove(SectionByPicture);
        }
    }
}