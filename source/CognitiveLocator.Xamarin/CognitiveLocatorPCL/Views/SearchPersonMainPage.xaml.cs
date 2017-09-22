using System;
using System.Collections.Generic;
using Microsoft.Azure.Mobile.Analytics;
using Xamarin.Forms;

namespace CognitiveLocator.Views
{
    public partial class SearchPersonMainPage : BaseView
    {
        public SearchPersonMainPage()
        {
            InitializeComponent();
            Analytics.TrackEvent("View: Search Person");
        }
    }
}
