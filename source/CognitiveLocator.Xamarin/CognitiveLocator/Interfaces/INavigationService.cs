using System.Threading.Tasks;

namespace CognitiveLocator.Interfaces
{
    public interface INavigationService
    {
        Task PopAsync();

        Task PopModalAsync();

        Task PushModalAsync(Page page);

        Task PopToRootAsync();

        Task PushAsync(Page page);
    }
}