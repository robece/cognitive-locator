using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CognitiveLocator.Models;
using CognitiveLocator.Services;
using CognitiveLocator.Views;
using Xamarin.Forms;

namespace CognitiveLocator.ViewModels
{
    public class SearchPersonResultViewModel : BaseViewModel
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

        bool _IsByPhoto;
        public bool IsByPhoto
        {
            get { return _IsByPhoto; }
            set { SetProperty(ref _IsByPhoto, value); }
        }

        bool _isRefreshing;
		public bool IsRefreshing
		{
			get { return _isRefreshing; }
			set { SetProperty(ref _isRefreshing, value); }
		}

        ObservableCollection<Person> _results;
        public ObservableCollection<Person> Results
        {
            get { return _results; }
            set { SetProperty(ref _results, value); }
        }

        public ICommand OnSelectedItemCommand
        {
            get;
            set;
        }
        public ICommand OnRefreshListCommand
        {
            get;
            set;
        }
        #endregion
        public SearchPersonResultViewModel(bool isPicture, byte[] picture, Person person) : base(new DependencyServiceBase())
        {
            Title = "Resultado de la búsqueda";
            IsByPhoto = isPicture;
            Photo = picture;
            Person = person;

            OnSelectedItemCommand = new Command<Person>(async (obj) => await OnItemSelected(obj));
            OnRefreshListCommand = new Command(async () =>
            {
                IsRefreshing = true;
                await LoadPersons();
                IsRefreshing = false;
            });
        }

        private async Task OnItemSelected(Person obj)
        {
            var page = new PersonDetailView(obj);

            await NavigationService.PushAsync(page);
        }

        #region Overrided Methods

        public async override Task OnViewAppear()
        {
            await LoadPersons();
        }

        #endregion

        #region Private Methods
        async Task LoadPersons()
        {
			if (!Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
			{
				await Application.Current.MainPage.DisplayAlert("Error", "Es necesario tener conexión a internet para continuar.", "Aceptar");
				await NavigationService.PopAsync();
			}
			IsBusy = true;
			var res = new List<Person>();

			if (!IsByPhoto)
			{
				res = await RestServices.SearchByNameAndLastNameAsync(Person);
			}
			else
			{
				var person = await RestServices.SearchPersonByPhotoAsync(Photo);
				if (person != null)
					res.Add(person);
			}

			Results = new ObservableCollection<Person>(res);

			if (!Results.Any())
			{
				await Application.Current.MainPage.DisplayAlert("Resultados", "No se encontro ninguna coincidencia.", "Aceptar");
				await NavigationService.PopAsync();
			}
			IsBusy = false;
        }
        #endregion
    }
}
