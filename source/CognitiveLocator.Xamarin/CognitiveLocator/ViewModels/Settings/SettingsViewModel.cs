using CognitiveLocator.Interfaces;
using CognitiveLocator.Pages;
using Xamarin.Forms;

namespace CognitiveLocator.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public Command LanguageSettingsCommand { get; set; }

        public SettingsViewModel() : this(new DependencyServiceBase())
        {
        }

        public SettingsViewModel(IDependencyService dependencyService) : base(dependencyService)
        {
            Title = Settings_Title;
            DependencyService = dependencyService;
            InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            LanguageSettingsCommand = new Command(() => GoToLanguageSettings());
        }

        private async void GoToLanguageSettings()
        {
            await NavigationService.PushAsync(new LanguageSettingsPage());
        }

        #region Binding Multiculture

        public string Settings_Title
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(Settings_Title), Resx.AppResources.Culture); }
        }

        public string Settings_Language
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(Settings_Language), Resx.AppResources.Culture); }
        }

        public string Settings_GeneralSettings
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(Settings_GeneralSettings), Resx.AppResources.Culture); }
        }

        #endregion
    }
}