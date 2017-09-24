using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CognitiveLocator.Models;
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
        #endregion
        public SearchPersonResultViewModel(bool isPicture, byte[] picture, Person person) : base(new DependencyServiceBase())
        {
            Title = "Buscar";
            IsByPhoto = isPicture;
            Photo = picture;
            Person = person;

            OnSelectedItemCommand = new Command<Person>(async (obj) => await OnItemSelected(obj));
        }

        private async Task OnItemSelected(Person obj)
        {
            var page = new PersonDetailView(obj);

            await NavigationService.PushAsync(page);
        }

        #region Overrided Methods

        public async override Task OnViewAppear()
        {
            var res = new List<Person>();

            if (!IsByPhoto)
            {
                if (!string.IsNullOrEmpty(Person.Name))
                {
                    res = await RestServices.SearchPersonByNameAsync(Person);
                }
                else
                {
                    res = await RestServices.SearchPersonByLastNameAsync(Person);
                }
            }
            else
            {
                res = await RestServices.SearchPersonByPhotoAsync(Photo);
            }

            Results = new ObservableCollection<Person>(res);

            return;
        }

        #endregion
    }
}
