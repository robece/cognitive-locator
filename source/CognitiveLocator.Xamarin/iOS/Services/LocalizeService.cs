using System;
using Xamarin.Forms;
using System.Threading;
using System.Globalization;
using CognitiveLocator.iOS.Services;
using CognitiveLocator.Interfaces;
using Foundation;

[assembly: Dependency(typeof(LocalizeService))]
namespace CognitiveLocator.iOS.Services
{
    public class LocalizeService : ILocalizeService
    {
        public void Set(string culture)
        {
            CultureInfo ci = new CultureInfo(culture);
            Resx.AppResources.Culture = ci;
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            Console.WriteLine("CurrentCulture set: " + ci.DisplayName);
        }
    }
}