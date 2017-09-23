using System;
using System.Collections.ObjectModel;
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
        public SearchPersonResultViewModel() : base(new DependencyServiceBase())
        {
            Title = "Buscar";

            OnSelectedItemCommand = new Command<Person>(async (obj) =>
            {
                var page = new PersonDetailPage(obj);
              

                await NavigationService.PushAsync(page);
            });
        }

        #region Overrided Methods
        public override System.Threading.Tasks.Task OnViewAppear()
        {
            // TODO: Load Results From WebAPI

            var res = new ObservableCollection<Person>();

            for (int i = 0; i < 5; i++)
            {
                res.Add(new Person
                {
                    Name = "Nombre",
                    LastName = "Apellido",
                    Age = new Random().Next(4, 40),
                    IsFound = 0,                    
                    Picture = "http://via.placeholder.com/150x150",
                    Location = "Hospital Angeles"
                });
            }

            Results = res;

            return base.OnViewAppear();
        }
        #endregion
    }
}
