using System;
using System.Collections.Generic;
using CognitiveLocator.ViewModels;
using Xamarin.Forms;

namespace CognitiveLocator.Views
{
    public partial class SearchPersonPage:BaseView
    {
        SearchPersonViewModel _vm;
        public SearchPersonViewModel ViewModel => _vm ?? (_vm = BindingContext as SearchPersonViewModel); 

        public SearchPersonPage()
        {
            InitializeComponent();
            BindingContext = new SearchPersonViewModel();
        }
    }
}
