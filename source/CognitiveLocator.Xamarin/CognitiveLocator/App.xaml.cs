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

            MainPage = new NavigationPage(new HomeView());

        }
    }
}
