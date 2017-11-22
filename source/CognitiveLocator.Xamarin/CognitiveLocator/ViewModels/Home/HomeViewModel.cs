using CognitiveLocator.Interfaces;
using CognitiveLocator.Pages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using CognitiveLocator.Domain;
using CognitiveLocator.Helpers;
using Newtonsoft.Json.Linq;

namespace CognitiveLocator.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public Command CreateReportCommand { get; set; }
        public Command SearchPersonNameCommand { get; set; }
        public Command SearchPersonPictureCommand { get; set; }
        public Command SettingsCommand { get; set; }
        public Command AboutCommand { get; set; }
        public Command LogoutCommand { get; set; }
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
            LogoutCommand = new Command(async () => await GoToLogout());

            this.IsBusy = true;
            Task.Run(async () =>
            {
                //initialize catalogs
                Catalogs.InitCountries();
                Catalogs.InitGenre();
                Catalogs.InitLanguages();

                //init facebook token
                var token = new JObject();
                token["access_token"] = DependencyService.Get<IAuthenticateService>().GetToken();

                //use facebook token on Azure Mobile App
                var authToken = await DependencyService.Get<IAuthenticateService>().Authenticate(token);
                Settings.MobileServiceAuthenticationToken = authToken;

                //get facebook profile
                var isAuthenticated = DependencyService.Get<IAuthenticateService>().IsAuthenticated();
                if (isAuthenticated)
                    DependencyService.Get<IAuthenticateService>().GetUserInfo();

                //get mobile settings
                Dictionary<string, string> result = await RestHelper.GetMobileSettings();
                Settings.MobileCenterID_Android = result[nameof(Settings.MobileCenterID_Android)];
                Settings.MobileCenterID_iOS = result[nameof(Settings.MobileCenterID_iOS)];
                Settings.AzureWebJobsStorage = result[nameof(Settings.AzureWebJobsStorage)];
                Settings.ImageStorageUrl = result[nameof(Settings.ImageStorageUrl)];

                //initialize App Center
                DependencyService.Get<IAppCenterService>().Initialize();

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

        private async Task GoToLogout()
        {
            await DependencyService.Get<IAuthenticateService>().ClearToken();
            Application.Current.MainPage = new LoginPage();
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

        public string Home_Logout
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(Home_Logout), Resx.AppResources.Culture); }
        }

        #endregion
    }
}