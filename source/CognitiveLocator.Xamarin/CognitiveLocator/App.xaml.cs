using CognitiveLocator.Interfaces;
using CognitiveLocator.Views;
using Xamarin.Forms;
using System.Reactive.Linq;
using Akavache;
using System.Globalization;
using CognitiveLocator.Domain;
using CognitiveLocator.Helpers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CognitiveLocator
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            LoadAppConfiguration();
            MainPage = new NavigationPage(new HomePage());
        }

        private async void LoadAppConfiguration()
        {
            List<Task> tasks = new List<Task>();
            Task<LanguageConfiguration> language_task = Task.Run(() => { return AkavacheHelper.GetUserAccountObject<LanguageConfiguration>(nameof(Settings.Language));});
            tasks.Add(language_task);
            Task.WaitAll(tasks.ToArray());

            LanguageConfiguration language = language_task.Result;

            if (language == null)
            {
                language = new LanguageConfiguration(Settings.Language);
                await AkavacheHelper.InsertUserAccountObject<LanguageConfiguration>(nameof(Settings.Language), language);
            }

            DependencyService.Get<ILocalize>().SetLocale(language.Language);
        }
    }
}