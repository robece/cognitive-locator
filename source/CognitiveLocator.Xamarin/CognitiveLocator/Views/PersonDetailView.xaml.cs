using System;
using System.Collections.Generic;
using CognitiveLocator.Domain;
using CognitiveLocator.ViewModels;
using Microsoft.Azure.Mobile.Analytics;
using Xamarin.Forms;

namespace CognitiveLocator.Views
{
    public partial class PersonDetailView : BaseView
    {
        PersonDetailViewModel _vm;
		public PersonDetailViewModel ViewModel => _vm ?? (_vm = BindingContext as PersonDetailViewModel);
		
        public PersonDetailView(Person person)
        {
            InitializeComponent();
            BindingContext = new PersonDetailViewModel(person);
            Analytics.TrackEvent("View: Person Detail");
        }
    }
}
