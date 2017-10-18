using CognitiveLocator.Interfaces;

namespace CognitiveLocator.ViewModels
{
    public class ReportConfirmationViewModel : BaseViewModel
    {
        public Command HomeCommand
        {
            get;
            set;
        }

        public ReportConfirmationViewModel() : base(new DependencyServiceBase())
        {
            InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            Title = "Gracias";
            HomeCommand = new Command(async () => await NavigationService.PopToRootAsync());
        }
    }
}