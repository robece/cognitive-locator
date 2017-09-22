using System;
using CognitiveLocator.Views;
using Xamarin.Forms;

namespace CognitiveLocator
{
    public partial class App : Application
    {
        public static bool UseMockDataStore = true;
        public static string BackendUrl = "https://localhost:5000";

        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<CloudDataStore>();

            if (Device.RuntimePlatform == Device.iOS)
                MainPage = new HomeView();
            else
                MainPage = new NavigationPage(new HomeView());
        }
    }
}
