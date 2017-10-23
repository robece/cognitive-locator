using CognitiveLocator.iOS.Services;
using MessageUI;
using UIKit;

[assembly: Dependency(typeof(EmailService))]

namespace CognitiveLocator.iOS.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string to, string subject)
        {
            var mailer = new MFMailComposeViewController();
            mailer.SetSubject(subject ?? string.Empty);
            mailer.SetToRecipients(new[] { to });
            mailer.Finished += (s, e) => ((MFMailComposeViewController)s).DismissViewController(true, () => { });

            UIViewController vc = UIApplication.SharedApplication.KeyWindow.RootViewController;
            while (vc.PresentedViewController != null)
            {
                vc = vc.PresentedViewController;
            }
            vc.PresentViewController(mailer, true, null);
        }
    }
}