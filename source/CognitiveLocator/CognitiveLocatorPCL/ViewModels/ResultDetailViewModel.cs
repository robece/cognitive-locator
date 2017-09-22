using System;

using Xamarin.Forms;

namespace CognitiveLocator.ViewModels
{
    public class ResultDetailViewModel : BaseViewModel
    {
        public ResultDetailViewModel() : this(new DependencyServiceBase())
        {
        }

        public ResultDetailViewModel(IDependencyService dependencyService) : base(dependencyService)
        {
			DependencyService = dependencyService;
		}
    }
}

