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

        public override System.Threading.Tasks.Task OnViewAppear()
        {
            this.Title = "Hola a todos!";
            return base.OnViewAppear();
        }
    }
}