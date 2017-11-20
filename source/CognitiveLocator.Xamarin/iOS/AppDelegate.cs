using Foundation;
using Microsoft.WindowsAzure.MobileServices;
using UIKit;
using Facebook.CoreKit;
using System.Collections.Generic;
using CognitiveLocator.iOS.Classes;

namespace CognitiveLocator.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public static MobileServiceClient MobileClient = null;
     
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Dictionary<string, object> dict = (Dictionary<string, object>)PListCSLight.readPlist("Info.plist");

            Profile.EnableUpdatesOnAccessTokenChange(true);
            Facebook.CoreKit.Settings.AppID = dict.GetValueOrDefault("FacebookAppID").ToString();
            Facebook.CoreKit.Settings.DisplayName = dict.GetValueOrDefault("FacebookDisplayName").ToString();

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            ApplicationDelegate.SharedInstance.FinishedLaunching(app, options);

            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
        }

        public override void OnActivated(UIApplication uiApplication)
        {
            base.OnActivated(uiApplication);
            AppEvents.ActivateApp();
        }
    }
}