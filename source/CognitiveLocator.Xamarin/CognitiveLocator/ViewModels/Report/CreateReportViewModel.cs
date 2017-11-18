using CognitiveLocator.Domain;
using CognitiveLocator.Helpers;
using CognitiveLocator.Interfaces;
using CognitiveLocator.Pages;
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

        List<string> countries = Catalogs.GetCountries();
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
                countriesSelectedIndex = value;

                // trigger some action to take such as updating other labels or fields
                OnPropertyChanged(nameof(CountriesSelectedIndex));

                SelectedCountryText = countries[CountriesSelectedIndex];
                SelectedCountry = Catalogs.GetCountryKey(SelectedCountryText);
            }
        }

        List<string> genre = Catalogs.GetGenre();
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
                genreSelectedIndex = value;

                // trigger some action to take such as updating other labels or fields
                OnPropertyChanged(nameof(GenreSelectedIndex));

                SelectedGenreText = genre[GenreSelectedIndex];
                SelectedGenre = Catalogs.GetGenreKey(SelectedGenreText);
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
            Title = CreateReport_Title;
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
            var confirmation = await Application.Current.MainPage.DisplayAlert(CreateReport_DisclaimerHeader, CreateReport_DisclaimerMessage, CreateReport_DisclaimerYes, CreateReport_DisclaimerNo);

            if (!confirmation)
                return;

            var model = ValidateInformation();
            if (!Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
                await Application.Current.MainPage.DisplayAlert(CreateReport_ConnectivityHeader, CreateReport_ConnectivityMessage, CreateReport_ConnectivityAccept);
            else if (!IsBusy)
            {
                IsBusy = true;
                var stream = new System.IO.MemoryStream(Photo);
                string NonAvailable = CreateReport_InformationNotAvailable;
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
                        await NavigationService.PushAsync(new CreateReportConfirmationPage());
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(CreateReport_ReportErrorHeader, CreateReport_ReportErrorMessage, CreateReport_ReportErrorAccept);
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(CreateReport_ReportErrorHeader, CreateReport_ReportErrorMessage, CreateReport_ReportErrorAccept);
                }

                IsBusy = false;
            }
        }

        private async Task PreviewReport()
        {
            if (!ValidateInformation())
                await Application.Current.MainPage.DisplayAlert(CreateReport_ValidationErrorHeader, CreateReport_ValidationErrorMessage, CreateReport_ValidationErrorAccept);
            else if (!IsBusy)
            {
                IsBusy = true;
                await NavigationService.PushAsync(new CreateReportPreviewPage(this));
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

        #region Binding Multiculture

        public string CreateReport_Title
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Title), Resx.AppResources.Culture); }
        }

        public string CreateReport_CameraText
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_CameraText), Resx.AppResources.Culture); }
        }

        public string CreateReport_GalleryText
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_GalleryText), Resx.AppResources.Culture); }
        }

        public string CreateReport_DisclaimerHeader
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_DisclaimerHeader), Resx.AppResources.Culture); }
        }

        public string CreateReport_DisclaimerMessage
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_DisclaimerMessage), Resx.AppResources.Culture); }
        }

        public string CreateReport_DisclaimerYes
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_DisclaimerYes), Resx.AppResources.Culture); }
        }

        public string CreateReport_DisclaimerNo
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_DisclaimerNo), Resx.AppResources.Culture); }
        }

        public string CreateReport_ConnectivityHeader
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_ConnectivityHeader), Resx.AppResources.Culture); }
        }

        public string CreateReport_ConnectivityMessage
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_ConnectivityMessage), Resx.AppResources.Culture); }
        }

        public string CreateReport_ConnectivityAccept
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_ConnectivityAccept), Resx.AppResources.Culture); }
        }

        public string CreateReport_InformationNotAvailable
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_InformationNotAvailable), Resx.AppResources.Culture); }
        }

        public string CreateReport_ReportErrorHeader
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_ReportErrorHeader), Resx.AppResources.Culture); }
        }

        public string CreateReport_ReportErrorMessage
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_ReportErrorMessage), Resx.AppResources.Culture); }
        }

        public string CreateReport_ReportErrorAccept
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_ReportErrorAccept), Resx.AppResources.Culture); }
        }

        public string CreateReport_ValidationErrorHeader
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_ValidationErrorHeader), Resx.AppResources.Culture); }
        }

        public string CreateReport_ValidationErrorMessage
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_ValidationErrorMessage), Resx.AppResources.Culture); }
        }

        public string CreateReport_ValidationErrorAccept
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_ValidationErrorAccept), Resx.AppResources.Culture); }
        }

        public string CreateReport_SectionPhoto
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_SectionPhoto), Resx.AppResources.Culture); }
        }

        public string CreateReport_SectionPhotoMessage
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_SectionPhotoMessage), Resx.AppResources.Culture); }
        }

        public string CreateReport_SectionGeneral
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_SectionGeneral), Resx.AppResources.Culture); }
        }

        public string CreateReport_Country
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Country), Resx.AppResources.Culture); }
        }

        public string CreateReport_ReportedBy
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_ReportedBy), Resx.AppResources.Culture); }
        }

        public string CreateReport_Name
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Name), Resx.AppResources.Culture); }
        }

        public string CreateReport_Lastname
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Lastname), Resx.AppResources.Culture); }
        }

        public string CreateReport_LocationOfLoss
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_LocationOfLoss), Resx.AppResources.Culture); }
        }

        public string CreateReport_DateOfLoss
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_DateOfLoss), Resx.AppResources.Culture); }
        }

        public string CreateReport_ReportId
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_ReportId), Resx.AppResources.Culture); }
        }

        public string CreateReport_PhysicalAttributes
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_PhysicalAttributes), Resx.AppResources.Culture); }
        }

        public string CreateReport_Genre
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Genre), Resx.AppResources.Culture); }
        }

        public string CreateReport_Complexion
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Complexion), Resx.AppResources.Culture); }
        }

        public string CreateReport_Skin
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Skin), Resx.AppResources.Culture); }
        }

        public string CreateReport_Front
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Front), Resx.AppResources.Culture); }
        }

        public string CreateReport_Mouth
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Mouth), Resx.AppResources.Culture); }
        }

        public string CreateReport_Eyebrows
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Eyebrows), Resx.AppResources.Culture); }
        }

        public string CreateReport_Age
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Age), Resx.AppResources.Culture); }
        }

        public string CreateReport_Height
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Height), Resx.AppResources.Culture); }
        }

        public string CreateReport_Face
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Face), Resx.AppResources.Culture); }
        }

        public string CreateReport_Nose
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Nose), Resx.AppResources.Culture); }
        }

        public string CreateReport_Lips
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Lips), Resx.AppResources.Culture); }
        }

        public string CreateReport_Chin
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Chin), Resx.AppResources.Culture); }
        }

        public string CreateReport_TypeColorEyes
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_TypeColorEyes), Resx.AppResources.Culture); }
        }

        public string CreateReport_TypeColorHair
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_TypeColorHair), Resx.AppResources.Culture); }
        }

        public string CreateReport_SectionAditionalInformation
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_SectionAditionalInformation), Resx.AppResources.Culture); }
        }

        public string CreateReport_ParticularSigns
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_ParticularSigns), Resx.AppResources.Culture); }
        }

        public string CreateReport_Clothes
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Clothes), Resx.AppResources.Culture); }
        }

        public string CreateReport_Next
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_Next), Resx.AppResources.Culture); }
        }

        public string CreateReport_PreviewMessage
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_PreviewMessage), Resx.AppResources.Culture); }
        }

        public string CreateReport_SendReport
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_SendReport), Resx.AppResources.Culture); }
        }

        #endregion
    }
}