using CognitiveLocator.Interfaces;
using CognitiveLocator.Services;
using CognitiveLocator.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using CognitiveLocator.Domain;

namespace CognitiveLocator.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public Command CreateReportCommand { get; set; }
        public Command SearchPersonNameCommand { get; set; }
        public Command SearchPersonPictureCommand { get; set; }
        public Command SettingsCommand { get; set; }
        public Command AboutCommand { get; set; }
        public Command NavigateToResultsCommand { get; set; }

        public HomeViewModel() : this(new DependencyServiceBase())
        {
        }

        public HomeViewModel(IDependencyService dependencyService) : base(dependencyService)
        {
            Title = Home_Title;
            DependencyService = dependencyService;
            InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            CreateReportCommand = new Command(async () => await GoToCreateReport());
            SearchPersonNameCommand = new Command(async () => await GoToSearchPersonByName());
            SearchPersonPictureCommand = new Command(async () => await GoToSearchPersonByPicture());
            SettingsCommand = new Command(async () => await GoToSettings());
            AboutCommand = new Command(async () => await GoToAbout());

            this.IsBusy = true;
            Task.Run(async () =>
            {
                Akavache.BlobCache.ApplicationName = nameof(Settings.CognitiveLocator);
                Catalogs.InitCountries();
                Catalogs.InitGenre();
                Catalogs.InitLanguages();
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

        private async Task GoToCreateReport()
        {
            await NavigationService.PushAsync(new CreateReportPage());
        }

        private async Task GoToSearchPersonByName()
        {
            await NavigationService.PushAsync(new SearchPersonPage("name"));
        }

        private async Task GoToSearchPersonByPicture()
        {
            await NavigationService.PushAsync(new SearchPersonPage("picture"));
        }

        private async Task GoToSettings()
        {
            await NavigationService.PushAsync(new SettingsPage());
        }

        private async Task GoToAbout()
        {
            await NavigationService.PushAsync(new AboutPage());
        }

        #region Binding Multiculture

        public string Home_Title
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(Home_Title), Resx.AppResources.Culture); }
        }

        public string Home_ReportPerson
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(Home_ReportPerson), Resx.AppResources.Culture); }
        }

        public string Home_NameSearch
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(Home_NameSearch), Resx.AppResources.Culture); }
        }

        public string Home_ImageSearch
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(Home_ImageSearch), Resx.AppResources.Culture); }
        }

        public string Home_Settings
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(Home_Settings), Resx.AppResources.Culture); }
        }

        public string Home_About
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(Home_About), Resx.AppResources.Culture); }
        }

        #endregion
    }
}