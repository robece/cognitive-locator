using Foundation;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using UIKit;

namespace CognitiveLocator.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //Telemetry on Mobile Center.
            MobileCenter.Start(Settings.MobileCenterID_iOS, typeof(Analytics));

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}