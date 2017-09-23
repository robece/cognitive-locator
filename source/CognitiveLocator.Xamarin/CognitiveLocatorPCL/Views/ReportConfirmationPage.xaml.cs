using System;
using System.Collections.Generic;
using Microsoft.Azure.Mobile.Analytics;
using Xamarin.Forms;

namespace CognitiveLocator.Views
{
    public partial class ReportConfirmationPage : ContentPage
    {
        public ReportConfirmationPage()
        {
            InitializeComponent();
            Analytics.TrackEvent("View: Report Confirmation");
        }
    }
}
