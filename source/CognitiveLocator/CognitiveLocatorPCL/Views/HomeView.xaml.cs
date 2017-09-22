using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CognitiveLocator.Views
{
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel();
        }
    }
}
