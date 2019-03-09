using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace CognitiveLocator
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }


        #region Setting Constants

        private static readonly string SettingsDefault = string.Empty;

        private const string LanguageKey = "LanguageKey";
        private const string FunctionURLKey = "FunctionURLKey";
        private const string CryptographyKey = "CryptographyKey";
        private const string AzureWebJobsStorageKey = "AzureWebJobsStorageKey";
        private const string MobileCenterID_AndroidKey = "MobileCenterID_AndroidKey";
        private const string MobileCenterID_iOSKey = "MobileCenterID_iOSKey";
        private const string NotificationAccessSignatureKey = "NotificationAccessSignatureKey";
        private const string NotificationHubNameKey = "NotificationHubNameKey";
        private const string ImageStorageUrlKey = "ImageStorageUrlKey";
        private const string MobileServiceAuthenticationTokenKey = "MobileServiceAuthenticationTokenKey";
        private const string FacebookProfileIdKey = "FacebookProfileIdKey";
        private const string FacebookProfileNameKey = "FacebookProfileNameKey";

        #endregion

        public static string Language
        {
            get
            {
                return AppSettings.GetValueOrDefault(LanguageKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(LanguageKey, value);
            }
        }

        public static string FunctionURL
        {
            get
            {
                return AppSettings.GetValueOrDefault(FunctionURLKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(FunctionURLKey, value);
            }
        }

        public static string Cryptography
        {
            get
            {
                return AppSettings.GetValueOrDefault(CryptographyKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(CryptographyKey, value);
            }
        }

        public static string AzureWebJobsStorage
        {
            get
            {
                return AppSettings.GetValueOrDefault(AzureWebJobsStorageKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AzureWebJobsStorageKey, value);
            }
        }

        public static string MobileCenterID_Android
        {
            get
            {
                return AppSettings.GetValueOrDefault(MobileCenterID_AndroidKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(MobileCenterID_AndroidKey, value);
            }
        }

        public static string MobileCenterID_iOS
        {
            get
            {
                return AppSettings.GetValueOrDefault(MobileCenterID_iOSKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(MobileCenterID_iOSKey, value);
            }
        }

        public static string NotificationAccessSignature
        {
            get
            {
                return AppSettings.GetValueOrDefault(NotificationAccessSignatureKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(NotificationAccessSignatureKey, value);
            }
        }

        public static string NotificationHubName
        {
            get
            {
                return AppSettings.GetValueOrDefault(NotificationHubNameKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(NotificationHubNameKey, value);
            }
        }

        public static string ImageStorageUrl
        {
            get
            {
                return AppSettings.GetValueOrDefault(ImageStorageUrlKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(ImageStorageUrlKey, value);
            }
        }

        public static string MobileServiceAuthenticationToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(MobileServiceAuthenticationTokenKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(MobileServiceAuthenticationTokenKey, value);
            }
        }

        public static string FacebookProfileId
        {
            get
            {
                return AppSettings.GetValueOrDefault(FacebookProfileIdKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(FacebookProfileIdKey, value);
            }
        }

        public static string FacebookProfileName
        {
            get
            {
                return AppSettings.GetValueOrDefault(FacebookProfileNameKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(FacebookProfileNameKey, value);
            }
        }
    }
}