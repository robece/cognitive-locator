using CognitiveLocator.Pages.Controls;
using CognitiveLocator.iOS;
using Xamarin.Forms.Platform.iOS;
using Facebook.LoginKit;
using Xamarin.Forms;
using UIKit;
using Facebook.CoreKit;

[assembly: ExportRenderer(typeof(FacebookLoginNative), typeof(FacebookLoginButtonRenderer))]
namespace CognitiveLocator.iOS
{
    public class FacebookLoginButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                UIButton button = Control;

                button.TouchUpInside += delegate
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
                var window = UIApplication.SharedApplication.KeyWindow;
                var vc = window.RootViewController;
                while (vc.PresentedViewController != null)
                {
                    vc = vc.PresentedViewController;
                }

                var manager = new LoginManager();
                manager.LoginBehavior = LoginBehavior.Web;
                manager.LogInWithReadPermissions(new string[] { "public_profile" },
                                                 vc,
                                                 (result, error) =>
                                                 {
                                                     if (error == null && !result.IsCancelled)
                                                     {
                                                         //proceed next page
                                                         App.ProceedToHome();
                                                     }
                                                 });
            }

        }
    }
}