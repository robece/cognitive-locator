using CognitiveLocator.Views;

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