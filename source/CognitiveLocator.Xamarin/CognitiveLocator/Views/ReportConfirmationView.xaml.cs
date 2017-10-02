using System;
using System.Collections.Generic;
using Microsoft.Azure.Mobile.Analytics;
using Xamarin.Forms;

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
