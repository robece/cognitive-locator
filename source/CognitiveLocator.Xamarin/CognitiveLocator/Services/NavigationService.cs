using CognitiveLocator.Interfaces;
using CognitiveLocator.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(NavigationService))]

namespace CognitiveLocator.Services
{
    public class NavigationService : INavigationService
    {
        public async Task PopModalAsync()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        public async Task PopAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task PopToRootAsync()
        {
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        public async Task PushAsync(Page page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public async Task PushModalAsync(Page page)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(page);
        }
    }
}