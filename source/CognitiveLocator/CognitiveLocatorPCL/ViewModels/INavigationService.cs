using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CognitiveLocator
{
    public interface INavigationService
    {
		Task PopModalAsync();

		Task PushModalAsync(Page page);

		Task PopToRootAsync();
    }
}
