using System;
using System.Threading.Tasks;
using CognitiveLocator.Services;
using Xamarin.Forms;

namespace CognitiveLocator.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public Command SendFeedbackCommand { get; set; }

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
            SendFeedbackCommand = new Command(() => SendEmailFeedback());
        }

        private void SendEmailFeedback()
        {
            DependencyService.Get<IEmailService>().SendEmail("rcervantes@outlook.com", "Comentario sobre Busca.me");
        }
    }
}
