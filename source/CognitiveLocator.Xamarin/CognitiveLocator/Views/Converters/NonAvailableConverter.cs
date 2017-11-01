using System;
using System.Globalization;
using Xamarin.Forms;

namespace CognitiveLocator.Views.Converters
{
    public class NonAvailableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string NonAvailable = CreateReport_InformationNotAvailable;
            return (string.IsNullOrEmpty((string)value)) ? NonAvailable : (string)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #region Binding Multiculture

        public string CreateReport_InformationNotAvailable
        {
            get { return Resx.AppResources.ResourceManager.GetString(nameof(CreateReport_InformationNotAvailable), Resx.AppResources.Culture); }
        }

        #endregion
    }
}