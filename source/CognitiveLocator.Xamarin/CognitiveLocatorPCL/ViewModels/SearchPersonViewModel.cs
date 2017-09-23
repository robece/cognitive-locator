using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CognitiveLocator.Models;
using CognitiveLocator.Views;
using Xamarin.Forms;

namespace CognitiveLocator.ViewModels
{
    public class SearchPersonViewModel : BaseViewModel
    {
        #region Properties

        Person _person;
        public Person Person
        {
            get { return _person; }
            set { SetProperty(ref _person, value); }
        }

        byte[] _photo;
        public byte[] Photo
        {
            get { return _photo; }
            set { SetProperty(ref _photo, value); }
        }

        string _searchType;
		public string SearchType
		{
			get { return _searchType; }
			set { SetProperty(ref _searchType, value); }
		}

        public ICommand SearchPersonByPictureCommand { get; set; }
        public ICommand SearchPersonByTextCommand { get; set; }
        public ICommand TakePhotoCommand { get; set; }
        public ICommand ChoosePhotoCommand { get; set; }
        #endregion

        public SearchPersonViewModel() : base(new DependencyServiceBase())
        {
            InitializeViewModel();
        }

		public SearchPersonViewModel(IDependencyService dependencyService) : base(dependencyService)
        {
			Title = "Bienvenido";
			DependencyService = dependencyService;
			InitializeViewModel();
		}

        void InitializeViewModel()
        {
            Title = "Buscar Persona";
            SearchPersonByPictureCommand = new Command(async () => await SearchPerson(true));
            SearchPersonByTextCommand = new Command(async () => await SearchPerson(false));
            TakePhotoCommand = new Command(async () => await TakePhoto());
            ChoosePhotoCommand = new Command(async () => await ChoosePhoto());
        }

        #region Tasks
        async Task SearchPerson(bool IsByPicture) {
            if(!IsBusy)
            {
                IsBusy = true;
				await Task.Delay(3000);

                if(IsByPicture){
                    //TO DO...
                }
                await NavigationService.PushAsync(new SearchPersonResultView());
            
				IsBusy = false;
            }
        }

        async Task TakePhoto()
        {
            Photo = await Helpers.MediaHelper.TakePhotoAsync();
        }

        async Task ChoosePhoto()
        {
            Photo = await Helpers.MediaHelper.PickPhotoAsync();
        }
        #endregion
    }
}

