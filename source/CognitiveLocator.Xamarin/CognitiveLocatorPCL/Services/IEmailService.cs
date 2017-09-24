using System;
namespace CognitiveLocator.Services
{
    public interface IEmailService
    {
        void SendEmail(string to, string subject);
    }
}
