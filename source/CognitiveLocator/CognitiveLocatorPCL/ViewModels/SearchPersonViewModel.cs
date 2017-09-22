using System;
using CognitiveLocator.Models;

namespace CognitiveLocator.ViewModels
{
    public class SearchPersonViewModel: BaseViewModel
    {
        #region Properties
        Person _person;
        public Person Person
        {
            get { return _person; }
            set { SetProperty(ref _person, value); }
        }
        #endregion
        public SearchPersonViewModel()
        {
            Title = "Buscar";
        }
    }
}
