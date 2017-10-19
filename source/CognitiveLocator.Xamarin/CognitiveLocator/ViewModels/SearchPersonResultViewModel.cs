using CognitiveLocator.Domain;
using CognitiveLocator.Interfaces;
using CognitiveLocator.Services;
using CognitiveLocator.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using CognitiveLocator.Helpers;

namespace CognitiveLocator.ViewModels
{
    public class SearchPersonResultViewModel : BaseViewModel
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

        private bool _IsByPhoto;

        public bool IsByPhoto
        {
            get { return _IsByPhoto; }
            set { SetProperty(ref _IsByPhoto, value); }
        }

        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }

        private ObservableCollection<Person> _results;

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

        #endregion Properties

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

        #endregion Overrided Methods

        #region Private Methods

        private async Task LoadPersons()
        {
            if (!Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Es necesario tener conexión a internet para continuar.", "Aceptar");
                await NavigationService.PopAsync();
            }
            IsBusy = true;
            var result = new List<Person>();

            if (!IsByPhoto)
            {
                MetadataVerification metadata = new MetadataVerification();
                metadata.Country = Person.Country;
                metadata.Name = Person.Name;
                metadata.Lastname = Person.Lastname;
                metadata.Location = Person.Location;
                metadata.Alias = Person.Alias;
                metadata.ReportedBy = Person.ReportedBy;

                result = await RestServiceV2.MetadataVerification(metadata);
            }
            else
            {
                var stream = new System.IO.MemoryStream(Photo);

                if (await StorageHelper.UploadPhoto(stream, null, true))
                {
                    await NavigationService.PushAsync(new ReportConfirmationView());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No fue posible verificar a la persona, si el error persiste intenta más tarde .", "Aceptar");
                }
            }

            IsBusy = false;
        }

        #endregion Private Methods
    }
}