using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CognitiveLocator.Models;
using CognitiveLocator.Views;
using Xamarin.Forms;

namespace CognitiveLocator.ViewModels
{
    public class SearchPersonMainViewModel : BaseViewModel
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

        public ICommand SearchPersonCommand { get; set; }
        public ICommand TakePhotoCommand { get; set; }
        public ICommand ChoosePhotoCommand { get; set; }
        #endregion
        public SearchPersonMainViewModel() : base(new DependencyServiceBase())
        {
            InitializeViewModel();
        }

		public SearchPersonMainViewModel(IDependencyService dependencyService) : base(dependencyService)
        {
			Title = "Bienvenido";
			DependencyService = dependencyService;
			InitializeViewModel();
		}

        void InitializeViewModel()
        {
            Title = "Buscar Persona";
            SearchPersonCommand = new Command(async () => await SearchPerson());
            TakePhotoCommand = new Command(async () => await TakePhoto());
            ChoosePhotoCommand = new Command(async () => await ChoosePhoto());
        }

        #region Tasks
        async Task SearchPerson() {
            if(!IsBusy)
            {
                IsBusy = true;
				await Task.Delay(3000);
                await NavigationService.PushAsync(new SearchPersonResultPage());
				IsBusy = false;
            }
        }

        async Task TakePhoto()
        {
            Photo = await Helpers.MediaHelper.TakePhotoAsync();
        }

        async Task ChoosePhoto()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

