using CognitiveLocator.Domain;
using CognitiveLocator.Interfaces;
using CognitiveLocator.Views;
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

        #endregion Properties

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
            Title = "Buscar persona";
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
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor asegúrate de llenar todos los campos.", "Aceptar");
                return;
            }

            if ((!ValidateInformation()) && (!IsByPicture))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor asegúrate de llenar todos los campos.", "Aceptar");
            }
            else
            {
                if (!IsBusy)
                {
                    IsBusy = true;

                    var page = new SearchPersonResultView(IsByPicture, Photo, Person);
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

        #endregion Tasks
    }
}