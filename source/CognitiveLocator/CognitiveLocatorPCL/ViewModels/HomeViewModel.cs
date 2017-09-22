using System;

using Xamarin.Forms;

namespace CognitiveLocator.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
		public HomeViewModel(IDependencyService dependencyService) : base(dependencyService)
        {
			DependencyService = dependencyService;
		}
    }
}

