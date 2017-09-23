using System;
using System.Collections.Generic;
using CognitiveLocator.Models;
using CognitiveLocator.ViewModels;
using Microsoft.Azure.Mobile.Analytics;
using Xamarin.Forms;

namespace CognitiveLocator.Views
{
    public partial class SearchPersonResultPage:BaseView
    {
        SearchPersonResultViewModel _vm;
        public SearchPersonResultViewModel ViewModel => _vm ?? (_vm = BindingContext as SearchPersonResultViewModel); 

        public SearchPersonResultPage()
        {
            InitializeComponent();
            BindingContext = new SearchPersonResultViewModel();
            Analytics.TrackEvent("View: Search Person Results");
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem != null)
            {
                var person = e.SelectedItem as Person;

                if(ViewModel.OnSelectedItemCommand.CanExecute(person))
                {
                    ViewModel.OnSelectedItemCommand.Execute(person);

                    ((ListView)sender).SelectedItem = null;
                }
            }
        }
    }
}
