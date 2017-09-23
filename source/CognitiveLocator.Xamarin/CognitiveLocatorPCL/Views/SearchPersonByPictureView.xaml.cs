using System;
using System.Collections.Generic;
using CognitiveLocator.ViewModels;
using Microsoft.Azure.Mobile.Analytics;
using Xamarin.Forms;

namespace CognitiveLocator.Views
{
    public partial class SearchPersonByPictureView : BaseView
    {
        public SearchPersonByPictureView()
        {
            InitializeComponent();
            BindingContext = new SearchPersonByPictureViewModel();
            Analytics.TrackEvent("View: Search By Picture");
        }
    }
}