using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Plugin.Permissions;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using CognitiveLocator.Droid.Callbacks;

namespace CognitiveLocator.Droid
{
    [Activity(Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
    ScreenOrientation = ScreenOrientation.Portrait)] //This is what controls orientation
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static MobileServiceClient MobileClient = null;
        ICallbackManager callbackManager;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);

            //create the client instance, using the mobile app backend URL
            MainActivity.MobileClient = new MobileServiceClient(Settings.FunctionURL);

            //facebook implementation
            callbackManager = CallbackManagerFactory.Create();

            var loginCallback = new FacebookCallback<LoginResult>
            {
                HandleSuccess = (loginResult) =>
                {
                    //proceed next page
                    App.ProceedToHome();
                },
                HandleCancel = () =>
                {
                    //handle cancel  
                },
                HandleError = (loginError) =>
                {
                    //handle error       
                }
            };

            LoginManager.Instance.RegisterCallback(callbackManager, loginCallback);

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            callbackManager.OnActivityResult(requestCode, (int)resultCode, data);
        }
    }
}