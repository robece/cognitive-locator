using CognitiveLocator.Domain;
using CognitiveLocator.Interfaces;
using CognitiveLocator.Services;
using CognitiveLocator.Pages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using CognitiveLocator.Helpers;
using System;

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

        #endregion

        public SearchPersonResultViewModel(bool isPicture, byte[] picture, Person person) : base(new DependencyServiceBase())
        {
            Title = SearchPersonResult_Title;
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
            var page = new PersonDetailPage(obj);

            await NavigationService.PushAsync(page);
        }

        #region Overrided Methods

        public async override Task OnViewAppear()
        {
            await LoadPersons();
        }

        #endregion

        #region Private Methods

        private async Task LoadPersons()
        {
            if (!Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                await Application.Current.MainPage.DisplayAlert(SearchPersonResult_ConnectivityHeader, SearchPersonResult_ConnectivityMessage, SearchPersonResult_ConnectivityAccept);
                await NavigationService.PopAsync();
            }
            IsBusy = true;
            var result = new List<Person>();

            if (!IsByPhoto)
            {
                MetadataVerification metadata = new MetadataVerification();
                metadata.Name = Person.Name;
                metadata.Lastname = Person.Lastname;

                result = await RestHelper.MetadataVerification(metadata);
            }
            else
            {
                var stream = new System.IO.MemoryStream(Photo);

                var pid = Guid.NewGuid().ToString();
                var extension = "jpg";

                if (await StorageHelper.UploadPhoto(pid, stream, true))
                {
                    result = await RestHelper.ImageVerification($"{pid}.{extension}");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(SearchPersonResult_VerificationErrorHeader, SearchPersonResult_VerificationErrorMessage, SearchPersonResult_VerificationErrorAccept);
                }
            }

            Results = new ObservableCollection<Person>(result);

            if (!Results.Any())
            {
                await Application.Current.MainPage.DisplayAlert(SearchPersonResult_NoResultsHeader, SearchPersonResult_NoResultsMessage, SearchPersonResult_NoResultsAccept);
                await NavigationService.PopAsync();
            }

            IsBusy = false;
        }

        #endregion

        #region Binding Multiculture

        public string SearchPersonResult_Title
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(SearchPersonResult_Title), Resx.AppResources.Culture); }
        }

        public string SearchPersonResult_ConnectivityHeader
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(SearchPersonResult_ConnectivityHeader), Resx.AppResources.Culture); }
        }

        public string SearchPersonResult_ConnectivityMessage
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(SearchPersonResult_ConnectivityMessage), Resx.AppResources.Culture); }
        }

        public string SearchPersonResult_ConnectivityAccept
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(SearchPersonResult_ConnectivityAccept), Resx.AppResources.Culture); }
        }

        public string SearchPersonResult_NoResultsHeader
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(SearchPersonResult_NoResultsHeader), Resx.AppResources.Culture); }
        }

        public string SearchPersonResult_NoResultsMessage
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(SearchPersonResult_NoResultsMessage), Resx.AppResources.Culture); }
        }

        public string SearchPersonResult_NoResultsAccept
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(SearchPersonResult_NoResultsAccept), Resx.AppResources.Culture); }
        }

        public string SearchPersonResult_VerificationErrorHeader
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(SearchPersonResult_VerificationErrorHeader), Resx.AppResources.Culture); }
        }

        public string SearchPersonResult_VerificationErrorMessage
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(SearchPersonResult_VerificationErrorMessage), Resx.AppResources.Culture); }
        }

        public string SearchPersonResult_VerificationErrorAccept
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(SearchPersonResult_VerificationErrorAccept), Resx.AppResources.Culture); }
        }

        #endregion
    }
}