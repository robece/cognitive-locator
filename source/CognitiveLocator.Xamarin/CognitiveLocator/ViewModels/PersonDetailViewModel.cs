using CognitiveLocator.Domain;
using CognitiveLocator.Interfaces;

namespace CognitiveLocator.ViewModels
{
    public class PersonDetailViewModel : BaseViewModel
    {
        #region Properties

        private Person _currentPerson;

        public Person CurrentPerson
        {
            get { return _currentPerson; }
            set { SetProperty(ref _currentPerson, value); }
        }

        #endregion Properties

        public PersonDetailViewModel(Person person) : base(new DependencyServiceBase())
        {
            Title = "Detalle del reporte";
            CurrentPerson = person;
        }
    }
}