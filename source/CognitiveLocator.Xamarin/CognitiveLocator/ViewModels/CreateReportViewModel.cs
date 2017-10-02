using System;
using System.Threading.Tasks;
using CognitiveLocator.Models.ApiModels;
using CognitiveLocator.Services;
using CognitiveLocator.Views;
using Xamarin.Forms;

namespace CognitiveLocator.ViewModels
{
    public class CreateReportViewModel : BaseViewModel
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

        string alias;
        public string Alias
        {
            get { return alias; }
            set { SetProperty(ref alias, value); }
        }

        DateTime? birthday = null;
        public DateTime? Birthday
        {
            get { return birthday; }
            set { SetProperty(ref birthday, value); }
        }

        string location;
        public string Location
        {
            get { return location; }
            set { SetProperty(ref location, value); }
        }

        string notes;
        public string Notes
        {
            get { return notes; }
            set { SetProperty(ref notes, value); }
        }

        string reportedby;
        public string ReportedBy
        {
            get { return reportedby; }
            set { SetProperty(ref reportedby, value); }
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
            Title = "Reportar persona";
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
            var confirmation = await Application.Current.MainPage.DisplayAlert("Envío de información", "Confirmo que de manera bien intensionada estoy compartiendo una fotografía y datos personales de una persona extraviada.", "Si", "No");

            if (!confirmation)
                return;

            var model = ValidateInformation();
			if (!Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
				await Application.Current.MainPage.DisplayAlert("Error", "Es necesario tener conexión a internet para continuar.", "Aceptar");
			else if (!IsBusy)
            {
                IsBusy = true;
                if(await RestServices.CreateReportAsync(model, Photo))
                    await NavigationService.PushAsync(new ReportConfirmationView());
                else
					await Application.Current.MainPage.DisplayAlert("Error", "No fue posible registrar el reporte, si el error persiste intenta mas tarde .", "Aceptar");

				IsBusy = false;

            }
        }

        private async Task PreviewReport()
        {
			var model = ValidateInformation();

			if (model == null)
				await Application.Current.MainPage.DisplayAlert("Error", "Por favor ingrese todo los datos obligatorios.", "Aceptar");
			else if (!IsBusy)
            {
                IsBusy = true;
                await NavigationService.PushAsync(new PreviewView(this));
                IsBusy = false;
            }
        }

        private CreateReportModel ValidateInformation()
        {
            var model = new CreateReportModel()
            {
                Name = this.Name,
                LastName = this.LastName,
                Alias = this.Alias,
                BirthDate = this.Birthday,
                Location = this.Location,
                Notes = this.Notes,
                ReportedBy = this.ReportedBy
            };

            if (Photo == null)
                return null;
            if (String.IsNullOrEmpty(model.Name))
                return null;
            if (String.IsNullOrEmpty(model.LastName))
                return null;
            if (String.IsNullOrEmpty(model.Location))
				return null;
            if (String.IsNullOrEmpty(model.ReportedBy))
                return null;
            
            return model;
        }
    }
}