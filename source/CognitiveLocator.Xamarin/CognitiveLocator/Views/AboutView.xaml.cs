using CognitiveLocator.ViewModels;

namespace CognitiveLocator.Views
{
    public partial class AboutView : BaseView
    {
        public AboutView()
        {
            InitializeComponent();
            BindingContext = new AboutViewModel();
        }
    }
}