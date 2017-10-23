using CognitiveLocator.ViewModels;
using Microsoft.Azure.Mobile.Analytics;

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