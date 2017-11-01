using CognitiveLocator.Interfaces;
using Xamarin.Forms;

namespace CognitiveLocator.ViewModels
{
    public class CreateReportConfirmationViewModel : BaseViewModel
    {
        public Command HomeCommand { get; set; }

        public CreateReportConfirmationViewModel() : this(new DependencyServiceBase())
        {
        }

        public CreateReportConfirmationViewModel(IDependencyService dependencyService) : base(dependencyService)
        {
            DependencyService = dependencyService;
            InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            Title = CreateReportConfirmation_Title;
            HomeCommand = new Command(() => GoToHome());
        }

        private async void GoToHome()
        {
            await NavigationService.PopToRootAsync();
        }

        #region Binding Multiculture

        public string CreateReportConfirmation_Title
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReportConfirmation_Title), Resx.AppResources.Culture); }
        }

        public string CreateReportConfirmation_P1
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReportConfirmation_P1), Resx.AppResources.Culture); }
        }

        public string CreateReportConfirmation_P2
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReportConfirmation_P2), Resx.AppResources.Culture); }
        }

        public string CreateReportConfirmation_ButtonHome
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReportConfirmation_ButtonHome), Resx.AppResources.Culture); }
        }

        #endregion
    }
}