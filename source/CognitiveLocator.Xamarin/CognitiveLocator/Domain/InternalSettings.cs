using System;
namespace CognitiveLocator.Domain
{
    public class InternalSettings
    {
        public string CognitiveLocator { get; set; }
        public string Language { get; set; }
        public string FunctionURL { get; set; }
        public string CryptographyKey { get; set; }
        public string AzureWebJobsStorage { get; set; }
        public string MobileCenterID_Android { get; set; }
        public string MobileCenterID_iOS { get; set; }
        public string NotificationAccessSignature { get; set; }
        public string NotificationHubName { get; set; }
        public string ImageStorageUrl { get; set; }
        public string MobileServiceAuthenticationToken { get; set; }
        public FacebookProfileData FacebookProfile { get; set; }
    }
}