using System;
using System.Collections.Generic;
using CognitiveLocator.ViewModels;
using Xamarin.Forms;

namespace CognitiveLocator.Views
{
    public partial class AboutView : BaseView
    {
        public AboutView()
        {
            InitializeComponent();
            BindingContext = new AboutViewModel();
        }
    }
}
