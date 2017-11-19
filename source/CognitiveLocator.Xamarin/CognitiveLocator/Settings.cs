using CognitiveLocator.Domain;

namespace CognitiveLocator
{
    public class Settings
    {
#if DEBUG
        public const string FunctionURL = "https://cognitivelocatortemp-fun.azurewebsites.net";
#else
        public const string FunctionURL = "https://cognitivelocatortemp-fun.azurewebsites.net";
#endif

        public static string CognitiveLocator = "CognitiveLocator";
        public static string Language = "en-US";

        public static string CryptographyKey = "aadaf729-2499-4b7e";
        public static string AzureWebJobsStorage = string.Empty;
        public static string MobileCenterID_Android = string.Empty;
        public static string MobileCenterID_iOS = string.Empty;
        public static string NotificationAccessSignature = string.Empty;
        public static string NotificationHubName = string.Empty;
        public static string ImageStorageUrl = string.Empty;

        public static string FacebookAppId = "1812003772432184";
        public static string FacebookAppName = "Locator";
        public static string MobileServiceAuthenticationToken = string.Empty;
        public static FacebookProfileData FacebookProfile = new FacebookProfileData();
    }
}