using System;
using System.Collections.ObjectModel;
using CognitiveLocator.Models;

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

        ObservableCollection<Person> _results;
        public ObservableCollection<Person> Results
        {
            get { return _results; }
            set { SetProperty(ref _results, value); }
        }
        #endregion
        public SearchPersonViewModel() : base(new DependencyServiceBase())
        {
            Title = "Buscar";
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
                    Nombre = "nombre",
                    Apellidos = "apellidos",
                    PhotoURL = "http://via.placeholder.com/150x150"
                });
            }

            Results = res;

            return base.OnViewAppear();
        }
        #endregion
    }
}
