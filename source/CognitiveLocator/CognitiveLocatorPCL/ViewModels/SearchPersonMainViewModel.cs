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
        public ICommand SearchPersonCommand { get; set; }
        public ICommand TakePhotoCommand { get; set; }
        public ICommand ChoosePhotoCommand { get; set; }
        #endregion
        public SearchPersonMainViewModel() : base(new DependencyServiceBase())
        {
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
				await NavigationService.PushAsync(new SearchPersonPage());
				IsBusy = false;
            }
        }

        async Task TakePhoto()
        {
            throw new NotImplementedException();
        }

        async Task ChoosePhoto()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

