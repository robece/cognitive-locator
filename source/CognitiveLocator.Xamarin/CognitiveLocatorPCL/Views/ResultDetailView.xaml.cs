using System;
using System.Collections.Generic;
using CognitiveLocator.ViewModels;
using Microsoft.Azure.Mobile.Analytics;
using Xamarin.Forms;

namespace CognitiveLocator.Views
{
    public partial class ResultDetailView : BaseView
    {
        public ResultDetailView()
        {
            InitializeComponent();
            BindingContext = new ResultDetailViewModel();
            Analytics.TrackEvent("View: Result Detail");
        }
    }
}
