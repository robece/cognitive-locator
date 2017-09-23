using System;
namespace CognitiveLocator.ViewModels
{
    public class SearchPersonByPictureViewModel : BaseViewModel
    {
		public SearchPersonByPictureViewModel() : this(new DependencyServiceBase())
        {

		}

		public SearchPersonByPictureViewModel(IDependencyService dependencyService) : base(dependencyService)
        {
			Title = "Bienvenido";
			DependencyService = dependencyService;
			InitializeViewModel();
		}

        private void InitializeViewModel()
        {
            
        }
    }
}
