using System;
using System.Collections.Generic;
using CognitiveLocator.ViewModels;
using Microsoft.Azure.Mobile.Analytics;
using Xamarin.Forms;

namespace CognitiveLocator.Views
{
    public partial class HomeView : BaseView
    {
        public HomeView()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel();
            Analytics.TrackEvent("View: Home");
        }
    }
}