using System;
using CognitiveLocator.ViewModels;
using Xamarin.Forms;
using Microsoft.AppCenter.Analytics;

namespace CognitiveLocator.Pages
{
    public partial class AboutPage : BasePage
    {
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = new AboutViewModel();

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                Uri uri = new Uri("https://cognitivelocator.github.io/");
                Device.OpenUri(uri);
            };
            websiteLink.GestureRecognizers.Add(tapGestureRecognizer);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Analytics.TrackEvent("View: About");
        }
    }
}