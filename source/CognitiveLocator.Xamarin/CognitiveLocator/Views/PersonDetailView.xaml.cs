using CognitiveLocator.Domain;
using CognitiveLocator.ViewModels;

namespace CognitiveLocator.Views
{
    public partial class PersonDetailView : BaseView
    {
        private PersonDetailViewModel _vm;
        public PersonDetailViewModel ViewModel => _vm ?? (_vm = BindingContext as PersonDetailViewModel);

        public PersonDetailView(Person person)
        {
            InitializeComponent();
            BindingContext = new PersonDetailViewModel(person);
            Analytics.TrackEvent("View: Person Detail");
        }
    }
}