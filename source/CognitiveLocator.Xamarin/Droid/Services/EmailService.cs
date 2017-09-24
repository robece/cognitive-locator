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
        public void SendEmail(string to, string subject)
        {
			try
            {
				var intent = new Intent(Intent.ActionSendMultiple);

				intent.SetType("text/plain");
				intent.PutExtra(Intent.ExtraEmail, to);
				intent.PutExtra(Intent.ExtraSubject, subject ?? string.Empty);

				CrossCurrentActivity.Current.Activity.StartActivity(intent);
            }
            catch (System.Exception ex)
            {
			    
			}
        }
    }
}