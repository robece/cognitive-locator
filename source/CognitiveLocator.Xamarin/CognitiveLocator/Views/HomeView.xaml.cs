using CognitiveLocator.ViewModels;

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