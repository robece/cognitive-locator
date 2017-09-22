using System;
using CognitiveLocator.Views;
using Xamarin.Forms;

namespace CognitiveLocator
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
                MainPage = new HomeView();
            else
                MainPage = new NavigationPage(new HomeView());
        }
    }
}
