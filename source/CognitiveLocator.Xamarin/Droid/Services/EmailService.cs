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
        public void SendEmail(string mail)
        {
            var email = new Intent(Android.Content.Intent.ActionSend);
            email.PutExtra(Android.Content.Intent.ExtraEmail,
            new string[] { mail });

            email.PutExtra(Android.Content.Intent.ExtraSubject, "Contacto Busca.me");

            email.PutExtra(Intent.ExtraHtmlText, true);
            email.SetType("message/rfc822");
            Forms.Context.StartActivity(email);
        }
    }
}