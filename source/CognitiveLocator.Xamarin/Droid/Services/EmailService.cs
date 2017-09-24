using System.Collections.Generic;
using Android.Content;
using CognitiveLocator.Droid.Services;
using CognitiveLocator.Services;
using CognitiveLocator.Xamarin.Droid;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(EmailService))]
namespace CognitiveLocator.Droid.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string mail, string subject)
        {
            var email = new Intent(Android.Content.Intent.ActionSend);
            email.PutExtra(Android.Content.Intent.ExtraEmail,
            new string[] { mail });

            email.PutExtra(Android.Content.Intent.ExtraSubject, subject);

            email.PutExtra(Intent.ExtraHtmlText, true);
            email.SetType("message/rfc822");
            Forms.Context.StartActivity(email);
        }
    }
}