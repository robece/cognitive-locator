using System;
using System.Collections.Generic;
using CognitiveLocator.Models;
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
