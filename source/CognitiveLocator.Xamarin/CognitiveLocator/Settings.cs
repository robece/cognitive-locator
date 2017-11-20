using System;
using System.Reflection;
using System.Threading.Tasks;
using CognitiveLocator.Domain;
using CognitiveLocator.Helpers;
using Xamarin.Forms.Internals;

namespace CognitiveLocator
{
    public enum SettingsType
    {
        FunctionURL,
        CognitiveLocator,
        Language,
        CryptographyKey,
        AzureWebJobsStorage,
        MobileCenterID_Android,
        MobileCenterID_iOS,
        NotificationAccessSignature,
        NotificationHubName,
        ImageStorageUrl,
        MobileServiceAuthenticationToken,
        FacebookProfile
    }

    /*
        The purpose of this Settings class is that allows to persists in storage the data using an specific enumerator, 
        but doesn't allow to set values directly in the settings exposed to the app, the value is set automatically and will be
        ready to be consumed at any moment using Setting.Attribute.

        In case you want to add a new attribute, follow the next steps:
        1) Add the property name in the public enumerator: SettingsType. 
        2) Add the property field in the exposed settings:
           private static string newProperty = null;
           [InternalVariable(nameof(newProperty))]
           public static string NewProperty { get { return newProperty; } }
        3) Add the property field in the internal settings (InternalSettings.cs):
           public string NewProperty { get; set; }
        4) Add the property field to the Initialize method to mapped the existing value:
           Settings.newProperty = settings.NewProperty;
           
        Now you're able to set the new value in the app:
           await Settings.Set<string>(SettingsType.NewProperty, "");

        Or read the value stored without read the local storage:
           Settings.NewProperty

     */

    public class Settings
    {
        public static async Task Initialize()
        {
            InternalSettings settings = await AkavacheHelper.GetUserAccountObject<InternalSettings>("InternalSettings");
            if (settings == null)
                await AkavacheHelper.InsertUserAccountObject<InternalSettings>("InternalSettings", new InternalSettings());

            if (settings != null)
            {
                Settings.functionURL = settings.FunctionURL;
                Settings.cognitiveLocator = settings.CognitiveLocator;
                Settings.language = settings.Language;
                Settings.cryptographyKey = settings.CryptographyKey;
                Settings.azureWebJobsStorage = settings.AzureWebJobsStorage;
                Settings.mobileCenterID_Android = settings.MobileCenterID_Android;
                Settings.mobileCenterID_iOS = settings.MobileCenterID_iOS;
                Settings.notificationAccessSignature = settings.NotificationAccessSignature;
                Settings.notificationHubName = settings.NotificationHubName;
                Settings.imageStorageUrl = settings.ImageStorageUrl;
                Settings.mobileServiceAuthenticationToken = settings.MobileServiceAuthenticationToken;
                Settings.facebookProfile = settings.FacebookProfile;
            }
        }

        public static async Task Set<T>(SettingsType setting, T value)
        {
            //lookup for local settings in storage container
            InternalSettings settings = await AkavacheHelper.GetUserAccountObject<InternalSettings>("InternalSettings");

            //update local settings
            SetPropertyValue<T>(settings, setting.ToString(), value);

            //save in storage settings
            await AkavacheHelper.InsertUserAccountObject<InternalSettings>("InternalSettings", settings);

            //update settings in current scope to make it accesible in all application
            Type t = typeof(Settings);
            PropertyInfo propertyInfo = t.GetProperty(setting.ToString());
            object[] attrs = (object[])propertyInfo.GetCustomAttributes(true);
            InternalVariableAttribute internalAttr = propertyInfo.GetCustomAttribute(typeof(InternalVariableAttribute)) as InternalVariableAttribute;
            var field = t.GetField(internalAttr.InternalVariable);
            field.SetValue(null, value);
        }

        static void SetPropertyValue<T>(object obj, string propName, T propValue)
        {
            obj.GetType().GetProperty(propName).SetValue(obj, propValue);
        }

        private static string cognitiveLocator = string.Empty;
        [InternalVariable(nameof(cognitiveLocator))]
        public static string CognitiveLocator { get { return cognitiveLocator; } }

        private static string language = string.Empty;
        [InternalVariable(nameof(language))]
        public static string Language { get { return language; } }

        private static string functionURL = string.Empty;
        [InternalVariable(nameof(functionURL))]
        public static string FunctionURL { get { return functionURL; } }

        private static string cryptographyKey = string.Empty;
        [InternalVariable(nameof(cryptographyKey))]
        public static string CryptographyKey { get { return cryptographyKey; } }

        private static string azureWebJobsStorage = string.Empty;
        [InternalVariable(nameof(azureWebJobsStorage))]
        public static string AzureWebJobsStorage { get { return azureWebJobsStorage; } }

        private static string mobileCenterID_Android = string.Empty;
        [InternalVariable(nameof(mobileCenterID_Android))]
        public static string MobileCenterID_Android { get { return mobileCenterID_Android; } }

        private static string mobileCenterID_iOS = string.Empty;
        [InternalVariable(nameof(mobileCenterID_iOS))]
        public static string MobileCenterID_iOS { get { return mobileCenterID_iOS; } }

        private static string notificationAccessSignature = string.Empty;
        [InternalVariable(nameof(notificationAccessSignature))]
        public static string NotificationAccessSignature { get { return notificationAccessSignature; } }

        private static string notificationHubName = string.Empty;
        [InternalVariable(nameof(notificationHubName))]
        public static string NotificationHubName { get { return notificationHubName; } }

        private static string imageStorageUrl = string.Empty;
        [InternalVariable(nameof(imageStorageUrl))]
        public static string ImageStorageUrl { get { return imageStorageUrl; } }

        private static string mobileServiceAuthenticationToken = string.Empty;
        [InternalVariable(nameof(mobileServiceAuthenticationToken))]
        public static string MobileServiceAuthenticationToken { get { return mobileServiceAuthenticationToken; } }

        private static FacebookProfileData facebookProfile = null;
        [InternalVariable(nameof(facebookProfile))]
        public static FacebookProfileData FacebookProfile { get { return facebookProfile; } }

        [AttributeUsage(AttributeTargets.Property)]
        public class InternalVariableAttribute : Attribute
        {
            private string internalVariable;
            public virtual string InternalVariable
            {
                get { return internalVariable; }
            }

            public InternalVariableAttribute(string internalVariable)
            {
                this.internalVariable = internalVariable;
            }
        }
    }
}