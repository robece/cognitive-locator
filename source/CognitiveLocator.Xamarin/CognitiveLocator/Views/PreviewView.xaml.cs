using CognitiveLocator.ViewModels;
using Microsoft.Azure.Mobile.Analytics;

namespace CognitiveLocator.Views
{
    public partial class PreviewView : BaseView
    {
        public PreviewView()
        {
            InitializeComponent();
        }

        public PreviewView(BaseViewModel reportContext)
        {
            InitializeComponent();
            this.BindingContext = reportContext;
            Analytics.TrackEvent("View: Preview Page");
        }
    }
}