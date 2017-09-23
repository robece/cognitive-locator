using System;
using System.Collections.Generic;
using Microsoft.Azure.Mobile.Analytics;
using Xamarin.Forms;

namespace CognitiveLocator.Views
{
    public partial class PreviewPage : BaseView
    {
        public PreviewPage(BaseViewModel reportContext)
        {
            InitializeComponent();
            this.BindingContext = reportContext;
            Analytics.TrackEvent("View: Preview Page");
        }
    }
}
