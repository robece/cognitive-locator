using System;
using System.Threading.Tasks;
using CognitiveLocator.Views;
using Xamarin.Forms;

namespace CognitiveLocator.ViewModels
{
    public class ResultsViewModel : BaseViewModel
    {
        public Command NavigateToDetailCommand { get; set; }

		public ResultsViewModel() : this(new DependencyServiceBase())
		{
            
		}

        public ResultsViewModel(IDependencyService dependencyService) : base(dependencyService)
        {
			DependencyService = dependencyService;
			InitializeViewModel();
		}

		private void InitializeViewModel()
		{
			NavigateToDetailCommand = new Command(async () => await NavigateToDetail());
		}

		private async Task NavigateToDetail()
		{
            await NavigationService.PushModalAsync(new ResultDetailView());
		}

		public override Task OnViewAppear()
		{
			this.Title = "Hola nueva pagina!";
			return base.OnViewAppear();
		}
    }
}
