using System;

using Xamarin.Forms;

namespace CognitiveLocator.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel() : this(new DependencyServiceBase())
		{
            
		}

        public HomeViewModel(IDependencyService dependencyService) : base(dependencyService)
        {
			DependencyService = dependencyService;
		}
    }
}