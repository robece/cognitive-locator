using CognitiveLocator.Interfaces;
using System.Collections.Generic;
using Xamarin.Forms;
using CognitiveLocator.Domain;
using CognitiveLocator.Helpers;

namespace CognitiveLocator.ViewModels
{
    public class LanguageSettingsViewModel : BaseViewModel
    {
        public Command SaveChangesCommand { get; set; }

        List<string> languages = Catalogs.GetLanguages();
        public List<string> Languages => languages;

        private string selectedLanguage;
        public string SelectedLanguage
        {
            get { return selectedLanguage; }
            set { SetProperty(ref selectedLanguage, value); }
        }

        private string selectedLanguageText;
        public string SelectedLanguageText
        {
            get { return selectedLanguageText; }
            set { SetProperty(ref selectedLanguageText, value); }
        }

        int languagesSelectedIndex;
        public int LanguagesSelectedIndex
        {
            get
            {
                return languagesSelectedIndex;
            }
            set
            {
                if (value != -1)
                {
                    languagesSelectedIndex = value;

                    // trigger some action to take such as updating other labels or fields
                    OnPropertyChanged(nameof(LanguagesSelectedIndex));

                    SelectedLanguageText = languages[LanguagesSelectedIndex];
                    SelectedLanguage = Catalogs.GetLanguageKey(SelectedLanguageText);

                    SaveConfiguration();
                }
            }
        }

        public LanguageSettingsViewModel() : this(new DependencyServiceBase())
        {
        }

        public LanguageSettingsViewModel(IDependencyService dependencyService) : base(dependencyService)
        {
            Title = Settings_LanguageTitle;
            DependencyService = dependencyService;
            InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            SaveChangesCommand = new Command(() => SaveConfiguration());

            string language = Settings.Language;

            if (string.IsNullOrEmpty(language)){
                LanguagesSelectedIndex = 0;
            }else{
                LanguagesSelectedIndex= Catalogs.GetLanguageIndex(language);
            }
        }

        private void SaveConfiguration()
        {
            Settings.Language = SelectedLanguage;        
            DependencyService.Get<ILocalizeService>().Set(SelectedLanguage);
        }

        #region Binding Multiculture

        public string Settings_LanguageTitle
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(Settings_LanguageTitle), Resx.AppResources.Culture); }
        }

        public string Settings_Language
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(Settings_Language), Resx.AppResources.Culture); }
        }

        public string Settings_LanguageSelect
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(Settings_LanguageSelect), Resx.AppResources.Culture); }
        }

        #endregion
    }
}