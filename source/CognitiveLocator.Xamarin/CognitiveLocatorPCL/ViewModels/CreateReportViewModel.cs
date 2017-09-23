using System;
using System.Threading.Tasks;
using CognitiveLocator.Models;
using CognitiveLocator.Views;
using Xamarin.Forms;

namespace CognitiveLocator.ViewModels
{
    public class CreateReportViewModel:BaseViewModel
    {

        public Command SendReportCommand { get; set; }
        public Command TakePhotoCommand { get; set; }
        public Command ChoosePhotoCommand { get; set; }
        public Command PreviewReportCommand { get; set; }

		byte[] photo;
		public byte[] Photo
		{
            get { return photo; }
            set { SetProperty(ref photo, value); }
		}

		string name;
		public string Name
		{
			get { return name; }
			set { SetProperty(ref name, value); }
		}

		string lastName;
		public string LastName
		{
			get { return lastName; }
			set { SetProperty(ref lastName, value); }
		}

		string age;
		public string Age
		{
			get { return age; }
			set { SetProperty(ref age, value); }
		}

		string location;
		public string Location
		{
			get { return location; }
			set { SetProperty(ref location, value); }
		}

		public CreateReportViewModel() : this(new DependencyServiceBase())
        {

		}

		public CreateReportViewModel(IDependencyService dependencyService) : base(dependencyService)
        {
			DependencyService = dependencyService;
			InitializeViewModel();
		}

		private void InitializeViewModel()
		{
            Title = "Crear Reporte";
            PreviewReportCommand = new Command(async () => await PreviewReport());
			SendReportCommand = new Command(async () => await SendReport());
            TakePhotoCommand = new Command(async () => await TakePhoto());
            ChoosePhotoCommand = new Command(async () => await ChoosePhoto());
		}


        private async Task ChoosePhoto()
        {
            Photo = await Helpers.MediaHelper.PickPhotoAsync();
        }

        private async Task TakePhoto()
        {
            Photo = await Helpers.MediaHelper.TakePhotoAsync();
        }

        private async Task SendReport()
        {
            if(!IsBusy)
            {
                IsBusy = true;
                await Task.Delay(3000);
                await NavigationService.PushAsync(new ReportConfirmationPage());
                IsBusy = false;
            }
        }

		private async Task PreviewReport()
		{
			if (!IsBusy)
			{
				IsBusy = true;
				await Task.Delay(3000);
				await NavigationService.PushAsync(new PreviewPage(this));
				IsBusy = false;
			}
		}
    }
}
