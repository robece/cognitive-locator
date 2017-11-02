using CognitiveLocator.Interfaces;
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
            Title = About_Title;
            SendFeedbackCommand = new Command(() => SendEmailFeedback());
        }

        private void SendEmailFeedback()
        {
            DependencyService.Get<IEmailService>().SendEmail("rcervantes@outlook.com", About_Feedback);
        }

        #region Binding Multiculture

        public string About_Title
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(About_Title), Resx.AppResources.Culture); }
        }

        public string About_P1
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(About_P1), Resx.AppResources.Culture); }
        }

        public string About_P2
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(About_P2), Resx.AppResources.Culture); }
        }

        public string About_P3
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(About_P3), Resx.AppResources.Culture); }
        }

        public string About_P4
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(About_P4), Resx.AppResources.Culture); }
        }

        public string About_Team
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(About_Team), Resx.AppResources.Culture); }
        }

        public string About_Feedback
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(About_Feedback), Resx.AppResources.Culture); }
        }

        public string About_Link
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(About_Link), Resx.AppResources.Culture); }
        }

        public string About_About
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(About_About), Resx.AppResources.Culture); }
        }

        #endregion
    }
}