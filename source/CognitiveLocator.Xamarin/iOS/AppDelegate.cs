using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using UIKit;

namespace CognitiveLocator.Xamarin.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //Telemetry on Mobile Center.
            MobileCenter.Start("0da75977-ccf2-43fd-ba88-ae712f9a3568", typeof(Analytics));

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
