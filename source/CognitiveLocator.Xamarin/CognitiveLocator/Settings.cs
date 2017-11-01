namespace CognitiveLocator
{
    public class Settings
    {
        public static string CognitiveLocator = "CognitiveLocator";
        public static string Language = "en-US";

        public static string CryptographyKey = "CRYPT_KEY";
        public static string AzureWebJobsStorage = string.Empty;
        public static string MobileCenterID_Android = string.Empty;
        public static string MobileCenterID_iOS = string.Empty;
        public static string NotificationAccessSignature = string.Empty;
        public static string NotificationHubName = string.Empty;
        public static string ImageStorageUrl = string.Empty;

#if DEBUG
        public const string FunctionURL = "https://YOUR_AZUREFUNCTION.azurewebsites.net";
#else
        public const string FunctionURL = "https://YOUR_AZUREFUNCTION.azurewebsites.net";
#endif
    }
}