using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Facebook.Login.Widget;
using CognitiveLocator.Pages.Controls;
using CognitiveLocator.Droid;
using Xamarin.Facebook.Login;
using Xamarin.Facebook;
using Android.App;

[assembly: ExportRenderer(typeof(FacebookLoginNative), typeof(FacebookLoginButtonRenderer))]
namespace CognitiveLocator.Droid
{
    public class FacebookLoginButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Android.Widget.Button button = Control;

                button.Click += delegate
                {
                    HandleFacebookLoginClicked();
                };
            }
        }

        void HandleFacebookLoginClicked()
        {
            if (AccessToken.CurrentAccessToken != null)
            {
                //proceed next page
                App.ProceedToHome();
            }
            else
            {
                LoginManager.Instance.SetLoginBehavior(LoginBehavior.WebOnly);
                LoginManager.Instance.LogInWithReadPermissions(Forms.Context as Activity, new string[] { "public_profile" });
            }

        }
    }
}