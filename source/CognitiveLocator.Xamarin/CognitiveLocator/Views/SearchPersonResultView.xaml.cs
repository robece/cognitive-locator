using CognitiveLocator.Domain;
using CognitiveLocator.ViewModels;
using Microsoft.Azure.Mobile.Analytics;
using Xamarin.Forms;

namespace CognitiveLocator.Views
{
    public partial class SearchPersonResultView : BaseView
    {

        public SearchPersonResultView(bool isByPicture, byte[] picture, Person person)
        {
            InitializeComponent();
            BindingContext = new SearchPersonResultViewModel(isByPicture, picture, person);
            Analytics.TrackEvent("View: Search Person Results");
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var vm = BindingContext as SearchPersonResultViewModel;
            if(e.SelectedItem != null)
            {
                var person = e.SelectedItem as Person;

                if(vm.OnSelectedItemCommand.CanExecute(person))
                {
                    vm.OnSelectedItemCommand.Execute(person);

                    ((ListView)sender).SelectedItem = null;
                }
            }
        }
    }
}
