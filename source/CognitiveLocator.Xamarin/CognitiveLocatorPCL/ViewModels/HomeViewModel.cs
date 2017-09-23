using System;
using System.Threading.Tasks;
using CognitiveLocator.Views;
using Xamarin.Forms;

namespace CognitiveLocator.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public Command NavigateToResultsCommand { get; set; }
        public Command SearchPersonNameCommand { get; set; }
		public Command SearchPersonPictureCommand { get; set; }
		public Command CreateReportCommand { get; set; }
		public Command AboutCommand { get; set; }


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
            SearchPersonNameCommand = new Command(async () => await SearchPersonByName());
            SearchPersonPictureCommand= new Command(async () => await SearchPersonByPicture());
            CreateReportCommand = new Command(async () => await CreateReport());
            AboutCommand = new Command(async () => await GoToAbout());
        }

        private async Task GoToAbout()
        {
            await NavigationService.PushAsync(new AboutView());
        }

        private async Task CreateReport()
        {
            await NavigationService.PushAsync(new CreateReportPage());
        }

        private async Task SearchPersonByName()
        {
            await NavigationService.PushAsync(new SearchPersonMainPage("name"));
        }

		private async Task SearchPersonByPicture()
		{
            await NavigationService.PushAsync(new SearchPersonMainPage("picture"));
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