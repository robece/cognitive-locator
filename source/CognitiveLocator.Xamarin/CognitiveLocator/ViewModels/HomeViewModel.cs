using CognitiveLocator.Interfaces;
using CognitiveLocator.Services;
using CognitiveLocator.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CognitiveLocator.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public Command CreateReportCommand { get; set; }
        public Command SearchPersonNameCommand { get; set; }
        public Command SearchPersonPictureCommand { get; set; }
        public Command AboutCommand { get; set; }
        public Command NavigateToResultsCommand { get; set; }

        public HomeViewModel() : this(new DependencyServiceBase())
        {
        }

        public HomeViewModel(IDependencyService dependencyService) : base(dependencyService)
        {
            Title = "¿Necesitas ayuda?";
            DependencyService = dependencyService;
            InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            CreateReportCommand = new Command(async () => await CreateReport());
            SearchPersonNameCommand = new Command(async () => await SearchPersonByName());
            SearchPersonPictureCommand = new Command(async () => await SearchPersonByPicture());
            AboutCommand = new Command(async () => await GoToAbout());

            this.IsBusy = true;
            Task.Run(async () =>
            {
                Dictionary<string, string> result = await RestServiceV2.GetMobileSettings();
                Settings.MobileCenterID_Android = result[nameof(Settings.MobileCenterID_Android)];
                Settings.MobileCenterID_iOS = result[nameof(Settings.MobileCenterID_iOS)];
                Settings.AzureWebJobsStorage = result[nameof(Settings.AzureWebJobsStorage)];
                Settings.ImageStorageUrl = result[nameof(Settings.ImageStorageUrl)];
            }).ContinueWith((b) =>
            {
                this.IsBusy = false;
            });
        }

        private async Task CreateReport()
        {
            await NavigationService.PushAsync(new CreateReportView());
        }

        private async Task SearchPersonByName()
        {
            await NavigationService.PushAsync(new SearchPersonView("name"));
        }

        private async Task SearchPersonByPicture()
        {
            await NavigationService.PushAsync(new SearchPersonView("picture"));
        }

        private async Task GoToAbout()
        {
            await NavigationService.PushAsync(new AboutView());
        }
    }
}