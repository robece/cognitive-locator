using System;
using System.Collections.Generic;
using Microsoft.Azure.Mobile.Analytics;
using Xamarin.Forms;

namespace CognitiveLocator.Views
{
    public partial class CreateReportView : BaseView
    {
        public CreateReportView()
        {
            InitializeComponent();
            Analytics.TrackEvent("View: Create Report");
        }
    }
}
