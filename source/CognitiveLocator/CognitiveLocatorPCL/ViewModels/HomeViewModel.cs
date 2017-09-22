using System;
using System.Threading.Tasks;
using CognitiveLocator.Views;
using Xamarin.Forms;

namespace CognitiveLocator.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public Command NavigateToResultsCommand { get; set; }

        public HomeViewModel() : this(new DependencyServiceBase())
		{
            
		}

        public HomeViewModel(IDependencyService dependencyService) : base(dependencyService)
        {
			DependencyService = dependencyService;
            InitializeViewModel();
		}

        private void InitializeViewModel()
        {
            NavigateToResultsCommand = new Command(async () => await NavigateToResults());
        }

        private async Task NavigateToResults()
        {
            await NavigationService.PushModalAsync(new ResultsView());
        }

        public override Task OnViewAppear()
        {
            this.Title = "Hola a todos!";
            return base.OnViewAppear();
        }
    }
}