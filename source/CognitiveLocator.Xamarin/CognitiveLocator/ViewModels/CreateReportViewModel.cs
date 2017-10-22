using CognitiveLocator.Domain;
using CognitiveLocator.Helpers;
using CognitiveLocator.Interfaces;
using CognitiveLocator.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;

namespace CognitiveLocator.ViewModels
{
    public class CreateReportViewModel : BaseViewModel
    {
        public Command SendReportCommand { get; set; }
        public Command TakePhotoCommand { get; set; }
        public Command ChoosePhotoCommand { get; set; }
        public Command PreviewReportCommand { get; set; }

        List<string> countries = new List<string> { "México" };
        public List<string> Countries => countries;

        private string selectedCountry;
        public string SelectedCountry
        {
            get { return selectedCountry; }
            set { SetProperty(ref selectedCountry, value); }
        }

        private string selectedCountryText;
        public string SelectedCountryText
        {
            get { return selectedCountryText; }
            set { SetProperty(ref selectedCountryText, value); }
        }

        int countriesSelectedIndex;
        public int CountriesSelectedIndex
        {
            get
            {
                return countriesSelectedIndex;
            }
            set
            {
                //if (countriesSelectedIndex != value)
                //{
                countriesSelectedIndex = value;

                // trigger some action to take such as updating other labels or fields
                OnPropertyChanged(nameof(CountriesSelectedIndex));

                SelectedCountryText = countries[CountriesSelectedIndex];
                SelectedCountry = GetAbbreviations(CountriesSelectedIndex);
                //}
            }
        }

        private string GetAbbreviations(int idx)
        {
            var result = string.Empty;
            switch (idx)
            {
                case 0:
                    result = "MX";
                    break;
            }
            return result;
        }

        List<string> genre = new List<string> { "Hombre", "Mujer" };
        public List<string> Genre => genre;

        private string selectedGenre;
        public string SelectedGenre
        {
            get { return selectedGenre; }
            set { SetProperty(ref selectedGenre, value); }
        }

        private string selectedGenreText;
        public string SelectedGenreText
        {
            get { return selectedGenreText; }
            set { SetProperty(ref selectedGenreText, value); }
        }

        int genreSelectedIndex;
        public int GenreSelectedIndex
        {
            get
            {
                return genreSelectedIndex;
            }
            set
            {
                //if (genreSelectedIndex != value)
                //{
                genreSelectedIndex = value;

                // trigger some action to take such as updating other labels or fields
                OnPropertyChanged(nameof(GenreSelectedIndex));

                SelectedGenreText = genre[GenreSelectedIndex];
                SelectedGenre = (GenreSelectedIndex == 0) ? "H" : "M";
                //}
            }
        }

        private byte[] photo;
        public byte[] Photo
        {
            get { return photo; }
            set { SetProperty(ref photo, value); }
        }

        private string reportedBy;
        public string ReportedBy
        {
            get { return reportedBy; }
            set { SetProperty(ref reportedBy, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string lastname;
        public string Lastname
        {
            get { return lastname; }
            set { SetProperty(ref lastname, value); }
        }

        private string locationOfLoss;
        public string LocationOfLoss
        {
            get { return locationOfLoss; }
            set { SetProperty(ref locationOfLoss, value); }
        }

        private string dateOfLoss;
        public string DateOfLoss
        {
            get { return dateOfLoss; }
            set { SetProperty(ref dateOfLoss, value); }
        }

        private string reportId;
        public string ReportId
        {
            get { return reportId; }
            set { SetProperty(ref reportId, value); }
        }

        private string complexion;
        public string Complexion
        {
            get { return complexion; }
            set { SetProperty(ref complexion, value); }
        }

        private string skin;
        public string Skin
        {
            get { return skin; }
            set { SetProperty(ref skin, value); }
        }

        private string front;
        public string Front
        {
            get { return front; }
            set { SetProperty(ref front, value); }
        }

        private string mouth;
        public string Mouth
        {
            get { return mouth; }
            set { SetProperty(ref mouth, value); }
        }

        private string eyebrows;
        public string Eyebrows
        {
            get { return eyebrows; }
            set { SetProperty(ref eyebrows, value); }
        }

        private string age;
        public string Age
        {
            get { return age; }
            set { SetProperty(ref age, value); }
        }

        private string height;
        public string Height
        {
            get { return height; }
            set { SetProperty(ref height, value); }
        }

        private string face;
        public string Face
        {
            get { return face; }
            set { SetProperty(ref face, value); }
        }

        private string nose;
        public string Nose
        {
            get { return nose; }
            set { SetProperty(ref nose, value); }
        }

        private string lips;
        public string Lips
        {
            get { return lips; }
            set { SetProperty(ref lips, value); }
        }

        private string chin;
        public string Chin
        {
            get { return chin; }
            set { SetProperty(ref chin, value); }
        }

        private string typeColorEyes;
        public string TypeColorEyes
        {
            get { return typeColorEyes; }
            set { SetProperty(ref typeColorEyes, value); }
        }

        private string typeColorHair;
        public string TypeColorHair
        {
            get { return typeColorHair; }
            set { SetProperty(ref typeColorHair, value); }
        }

        private string particularSigns;
        public string ParticularSigns
        {
            get { return particularSigns; }
            set { SetProperty(ref particularSigns, value); }
        }

        private string clothes;
        public string Clothes
        {
            get { return clothes; }
            set { SetProperty(ref clothes, value); }
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

            CountriesSelectedIndex = 0;
            GenreSelectedIndex = 0;
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
                var stream = new System.IO.MemoryStream(Photo);
                string NonAvailable = "Información no disponible";
                var person = new Person
                {
                    Country = (string.IsNullOrEmpty(this.SelectedCountry)) ? NonAvailable : this.SelectedCountry,
                    ReportedBy = (string.IsNullOrEmpty(this.ReportedBy)) ? NonAvailable : this.ReportedBy,
                    Name = (string.IsNullOrEmpty(this.Name)) ? NonAvailable : this.Name,
                    Lastname = (string.IsNullOrEmpty(this.Lastname)) ? NonAvailable : this.Lastname,
                    LocationOfLoss = (string.IsNullOrEmpty(this.LocationOfLoss)) ? NonAvailable : this.LocationOfLoss,
                    DateOfLoss = (string.IsNullOrEmpty(this.DateOfLoss)) ? NonAvailable : this.DateOfLoss,
                    ReportId = (string.IsNullOrEmpty(this.ReportId)) ? NonAvailable : this.ReportId,
                    Genre = (string.IsNullOrEmpty(this.SelectedGenre)) ? NonAvailable : this.SelectedGenre,
                    Complexion = (string.IsNullOrEmpty(this.Complexion)) ? NonAvailable : this.Complexion,
                    Skin = (string.IsNullOrEmpty(this.Skin)) ? NonAvailable : this.Skin,
                    Front = (string.IsNullOrEmpty(this.Front)) ? NonAvailable : this.Front,
                    Mouth = (string.IsNullOrEmpty(this.Mouth)) ? NonAvailable : this.Mouth,
                    Eyebrows = (string.IsNullOrEmpty(this.Eyebrows)) ? NonAvailable : this.Eyebrows,
                    Age = (string.IsNullOrEmpty(this.Age)) ? NonAvailable : this.Age,
                    Height = (string.IsNullOrEmpty(this.Height)) ? NonAvailable : this.Height,
                    Face = (string.IsNullOrEmpty(this.Face)) ? NonAvailable : this.Face,
                    Nose = (string.IsNullOrEmpty(this.Nose)) ? NonAvailable : this.Nose,
                    Lips = (string.IsNullOrEmpty(this.Lips)) ? NonAvailable : this.Lips,
                    Chin = (string.IsNullOrEmpty(this.Chin)) ? NonAvailable : this.Chin,
                    TypeColorEyes = (string.IsNullOrEmpty(this.TypeColorEyes)) ? NonAvailable : this.TypeColorEyes,
                    TypeColorHair = (string.IsNullOrEmpty(this.TypeColorHair)) ? NonAvailable : this.TypeColorHair,
                    ParticularSigns = (string.IsNullOrEmpty(this.ParticularSigns)) ? NonAvailable : this.ParticularSigns,
                    Clothes = (string.IsNullOrEmpty(this.Clothes)) ? NonAvailable : this.Clothes
                };

                var pid = Guid.NewGuid().ToString();

                if (await StorageHelper.UploadMetadata(pid, person))
                {
                    if (await StorageHelper.UploadPhoto(pid, stream))
                    {
                        await NavigationService.PushAsync(new ReportConfirmationView());
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "No fue posible registrar el reporte, si el error persiste intenta más tarde.", "Aceptar");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No fue posible registrar el reporte, si el error persiste intenta más tarde.", "Aceptar");
                }

                IsBusy = false;
            }
        }

        private async Task PreviewReport()
        {
            if (!ValidateInformation())
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor ingrese todo los datos obligatorios.", "Aceptar");
            else if (!IsBusy)
            {
                IsBusy = true;
                await NavigationService.PushAsync(new PreviewView(this));
                IsBusy = false;
            }
        }

        private bool ValidateInformation()
        {
            if (Photo == null)
                return false;
            if (String.IsNullOrEmpty(ReportedBy))
                return false;
            if (String.IsNullOrEmpty(Name))
                return false;
            if (String.IsNullOrEmpty(Lastname))
                return false;
            if (String.IsNullOrEmpty(LocationOfLoss))
                return false;
            if (String.IsNullOrEmpty(DateOfLoss))
                return false;
            if (String.IsNullOrEmpty(ReportId))
                return false;

            return true;
        }
    }
}