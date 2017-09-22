using System;
using System.Collections.Generic;
using CognitiveLocator.ViewModels;
using Xamarin.Forms;

namespace CognitiveLocator.Views
{
    public partial class ResultsView : ContentPage
    {
        public ResultsView()
        {
            InitializeComponent();
            BindingContext = new ResultsViewModel();
        }
    }
}