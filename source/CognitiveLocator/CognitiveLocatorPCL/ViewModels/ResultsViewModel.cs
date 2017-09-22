using System;

using Xamarin.Forms;

namespace CognitiveLocator.ViewModels
{
    public class ResultsViewModel : BaseViewModel
    {
		public ResultsViewModel() : this(new DependencyServiceBase())
		{
            
		}

        public ResultsViewModel(IDependencyService dependencyService) : base(dependencyService)
        {
			DependencyService = dependencyService;
		}
    }
}
