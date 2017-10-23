using System;
using CognitiveLocator.ViewModels;
using Xamarin.Forms;

namespace CognitiveLocator.Views
{
    public partial class AboutView : BaseView
    {
        public AboutView()
        {
            InitializeComponent();
            BindingContext = new AboutViewModel();

            websiteLink.Text = "Visitanos en: cognitivelocator.github.io";

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                Uri uri = new Uri("https://cognitivelocator.github.io/");
                Device.OpenUri(uri);
            };
            websiteLink.GestureRecognizers.Add(tapGestureRecognizer);
        }
    }
}