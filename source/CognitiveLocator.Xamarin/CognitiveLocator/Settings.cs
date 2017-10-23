namespace CognitiveLocator
{
    public class Settings
    {
        public static string CryptographyKey = "aadaf729-2499-4b7e";
        public static string AzureWebJobsStorage = string.Empty;
        public static string MobileCenterID_Android = string.Empty;
        public static string MobileCenterID_iOS = string.Empty;
        public static string NotificationAccessSignature = string.Empty;
        public static string NotificationHubName = string.Empty;
        public static string ImageStorageUrl = string.Empty;

#if DEBUG
        public const string FunctionURL = "https://cognitivelocatordev-fun.azurewebsites.net";
#else
        public const string FunctionURL = "https://cognitivelocatordev-fun.azurewebsites.net";
#endif
    }
}