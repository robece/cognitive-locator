using CognitiveLocator.Droid.Services;
using CognitiveLocator.Interfaces;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureService))]
namespace CognitiveLocator.Droid.Services
{
    public class AzureService : IAzureService
    {
        public void Initialize()
        {
            //telemetry on Mobile Center
            MobileCenter.Start(Settings.MobileCenterID_Android, typeof(Analytics));
        }
    }
}