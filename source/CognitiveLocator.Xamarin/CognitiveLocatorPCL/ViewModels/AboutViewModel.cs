using System;
namespace CognitiveLocator.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
		public AboutViewModel() : this(new DependencyServiceBase())
        {

		}

		public AboutViewModel(IDependencyService dependencyService) : base(dependencyService)
        {
			DependencyService = dependencyService;
			InitializeViewModel();
		}

        private void InitializeViewModel()
        {
            Title = "Acerca de";
        }
    }
}
