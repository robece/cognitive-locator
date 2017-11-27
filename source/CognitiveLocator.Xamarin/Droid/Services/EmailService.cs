using Android.Content;
using CognitiveLocator.Droid.Services;
using Xamarin.Forms;
using CognitiveLocator.Interfaces;
using Plugin.CurrentActivity;

[assembly: Dependency(typeof(EmailService))]
namespace CognitiveLocator.Droid.Services
{
    public class EmailService : IEmailService
    {
        Context CurrentContext => CrossCurrentActivity.Current.Activity;

        public void SendEmail(string mail, string subject)
        {
            var email = new Intent(Android.Content.Intent.ActionSend);
            email.PutExtra(Android.Content.Intent.ExtraEmail,
            new string[] { mail });

            email.PutExtra(Android.Content.Intent.ExtraSubject, subject);

            email.PutExtra(Intent.ExtraHtmlText, true);
            email.SetType("message/rfc822");

            CurrentContext.StartActivity(email);
        }
    }
}