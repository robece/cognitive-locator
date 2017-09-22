using System;
using System.Collections.Generic;
using CognitiveLocator.ViewModels;
using Xamarin.Forms;

namespace CognitiveLocator.Views
{
    public partial class ResultDetailView : ContentPage
    {
        public ResultDetailView()
        {
            InitializeComponent();
            BindingContext = new ResultDetailViewModel();
        }
    }
}
