using CognitiveLocator.Models;
using CognitiveLocator.Services;

namespace CognitiveLocator.ViewModels
{
    public class PersonDetailViewModel : BaseViewModel
    {
        #region Properties
        Person _currentPerson;
        public Person CurrentPerson
        {
            get { return _currentPerson; }
            set { SetProperty(ref _currentPerson, value); }
        }
        #endregion

        public PersonDetailViewModel(Person person) : base(new DependencyServiceBase())
        {
            Title = "Detalle";
            CurrentPerson = person;
        }
    }
}
