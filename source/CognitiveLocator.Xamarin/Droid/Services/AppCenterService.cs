using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

[assembly: Dependency(typeof(CognitiveLocator.Droid.Services.AppCenterService))]
namespace CognitiveLocator.Droid.Services
{
    public class AppCenterService : Interfaces.IAppCenterService
    {
        public void Initialize()
        {
            //telemetry on Mobile Center
            AppCenter.Start(Settings.MobileCenterID_Android, typeof(Analytics), typeof(Crashes));
        }
    }
}