using System.Threading.Tasks;
using CognitiveLocator.Interfaces;
using CognitiveLocator.Pages;
using Xamarin.Forms;

namespace CognitiveLocator.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command NavigateToHomeCommand { get; set; }

        private bool isAuthenticated;
        public bool IsAuthenticated
        {
            get { return isAuthenticated; }
            set { SetProperty(ref isAuthenticated, value); }
        }


        public LoginViewModel() : this(new DependencyServiceBase())
        {
        }

        public LoginViewModel(IDependencyService dependencyService) : base(dependencyService)
        {
            DependencyService = dependencyService;
            InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            Title = Login_Title;
            IsAuthenticated = DependencyService.Get<IAuthenticateService>().IsAuthenticated();

            NavigateToHomeCommand = new Command(() => App.ProceedToHome());
        }

        #region Binding Multiculture

        public string Login_Title
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(Login_Title), Resx.AppResources.Culture); }
        }

        public string Login_Proceed
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(Login_Proceed), Resx.AppResources.Culture); }
        }

        public string Login_Login
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(Login_Login), Resx.AppResources.Culture); }
        }

        #endregion
    }
}