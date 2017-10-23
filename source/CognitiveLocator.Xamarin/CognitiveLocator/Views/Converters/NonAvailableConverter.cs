using System;
using System.Globalization;
using Xamarin.Forms;

namespace CognitiveLocator.Views.Converters
{
    public class NonAvailableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string NonAvailable = "Información no disponible";
            return (string.IsNullOrEmpty((string)value)) ? NonAvailable : (string)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}