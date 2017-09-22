using System;
using System.Threading.Tasks;
using CognitiveLocator.Views;
using Xamarin.Forms;

namespace CognitiveLocator.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public Command NavigateToResultsCommand { get; set; }
		public Command SearchPersonCommand { get; set; }
		public Command CreateReportCommand { get; set; }



		public HomeViewModel() : this(new DependencyServiceBase())
		{
            
		}

        public HomeViewModel(IDependencyService dependencyService) : base(dependencyService)
        {
            Title = "Bienvenido";
			DependencyService = dependencyService;
            InitializeViewModel();
		}

        private void InitializeViewModel()
        {
            NavigateToResultsCommand = new Command(async () => await NavigateToResults());
            SearchPersonCommand = new Command(async () => await SearchPerson());
            CreateReportCommand = new Command(async () => await CreateReport());
        }

        private async Task CreateReport()
        {
            await NavigationService.PushAsync(new SearchPersonPage());
        }

        private async Task SearchPerson()
        {
            await NavigationService.PushAsync(new CreateReportPage());
        }

        private async Task NavigateToResults()
        {
            var photo = await Helpers.MediaHelper.TakePhotoAsync();
            photo = await Helpers.MediaHelper.AdjustImageSize(photo);

        }

        public override Task OnViewAppear()
        {
            return base.OnViewAppear();
        }
    }
}