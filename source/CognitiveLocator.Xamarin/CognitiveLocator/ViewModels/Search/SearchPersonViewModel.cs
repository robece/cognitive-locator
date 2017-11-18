using CognitiveLocator.Domain;
using CognitiveLocator.Interfaces;
using CognitiveLocator.Pages;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System;

namespace CognitiveLocator.ViewModels
{
    public class SearchPersonViewModel : BaseViewModel
    {
        #region Properties

        private Person _person;

        public Person Person
        {
            get { return _person; }
            set { SetProperty(ref _person, value); }
        }

        private byte[] _photo;

        public byte[] Photo
        {
            get { return _photo; }
            set { SetProperty(ref _photo, value); }
        }

        private string _searchType;

        public string SearchType
        {
            get { return _searchType; }
            set { SetProperty(ref _searchType, value); }
        }

        public bool IsByPicture => (SearchType == "picture") ? true : false;

        public ICommand SearchPersonByPictureCommand { get; set; }
        public ICommand SearchPersonByNameCommand { get; set; }
        public ICommand TakePhotoCommand { get; set; }
        public ICommand ChoosePhotoCommand { get; set; }

        #endregion

        public SearchPersonViewModel() : base(new DependencyServiceBase())
        {
            InitializeViewModel();
        }

        public SearchPersonViewModel(IDependencyService dependencyService) : base(dependencyService)
        {
            DependencyService = dependencyService;
            InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            Title = SearchPerson_Title;
            Person = new Person();
            SearchPersonByPictureCommand = new Command(async () => await SearchPerson());
            SearchPersonByNameCommand = new Command(async () => await SearchPerson());
            TakePhotoCommand = new Command(async () => await TakePhoto());
            ChoosePhotoCommand = new Command(async () => await ChoosePhoto());
        }

        #region Tasks

        private async Task SearchPerson()
        {
            if (IsByPicture == true && Photo == null)
            {
                await Application.Current.MainPage.DisplayAlert(SearchPerson_ValidationHeader, SearchPerson_ValidationMessage, SearchPerson_ValidationAccept);
                return;
            }

            if ((!ValidateInformation()) && (!IsByPicture))
            {
                await Application.Current.MainPage.DisplayAlert(SearchPerson_ValidationHeader, SearchPerson_ValidationMessage, SearchPerson_ValidationAccept);
            }
            else
            {
                if (!IsBusy)
                {
                    IsBusy = true;

                    var page = new SearchPersonResultPage(IsByPicture, Photo, Person);
                    await NavigationService.PushAsync(page);

                    IsBusy = false;
                }
            }
        }

        private bool ValidateInformation()
        {
            if (String.IsNullOrEmpty(Person.Name))
                return false;
            if (String.IsNullOrEmpty(Person.Lastname))
                return false;
            return true;
        }

        private async Task TakePhoto()
        {
            Photo = await Helpers.MediaHelper.TakePhotoAsync();
        }

        private async Task ChoosePhoto()
        {
            Photo = await Helpers.MediaHelper.PickPhotoAsync();
        }

        #endregion

        #region Binding Multiculture

        public string SearchPerson_Title
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(SearchPerson_Title), Resx.AppResources.Culture); }
        }

        public string SearchPerson_ValidationHeader
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(SearchPerson_ValidationHeader), Resx.AppResources.Culture); }
        }

        public string SearchPerson_ValidationMessage
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(SearchPerson_ValidationMessage), Resx.AppResources.Culture); }
        }

        public string SearchPerson_ValidationAccept
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(SearchPerson_ValidationAccept), Resx.AppResources.Culture); }
        }

        public string SearchPerson_SectionPhoto
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(SearchPerson_SectionPhoto), Resx.AppResources.Culture); }
        }

        public string SearchPerson_SectionPhotoMessage
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(SearchPerson_SectionPhotoMessage), Resx.AppResources.Culture); }
        }

        public string SearchPerson_CameraText
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(SearchPerson_CameraText), Resx.AppResources.Culture); }
        }

        public string SearchPerson_GalleryText
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(SearchPerson_GalleryText), Resx.AppResources.Culture); }
        }

        public string SearchPerson_SearchText
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(SearchPerson_SearchText), Resx.AppResources.Culture); }
        }

        public string SearchPerson_SectionPersonData
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(SearchPerson_SectionPersonData), Resx.AppResources.Culture); }
        }

        public string SearchPerson_Name
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(SearchPerson_Name), Resx.AppResources.Culture); }
        }

        public string SearchPerson_Lastname
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(SearchPerson_Lastname), Resx.AppResources.Culture); }
        }

        #endregion
    }
}