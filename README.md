# Cognitive Locator

## Build Status

| App        | Status          |
| ---------- | --------------- |
| Cognitive Locator (Droid)  | ![Build status][app-data-build-status-droid-master] |
| Cognitive Locator (iOS)  | ![Build status][app-data-build-status-ios-master] |

[![Deploy to Azure](https://azuredeploy.net/deploybutton.png)](https://azuredeploy.net/)

[app-data-build-status-droid-master]:https://build.mobile.azure.com/v0.1/apps/596b9007-8b42-4325-9a49-0251fa24cc87/branches/master/badge

[app-data-build-status-ios-master]:https://build.mobile.azure.com/v0.1/apps/346c8576-7ceb-4b6b-8951-8846b42fdc68/branches/master/badge

[app-data-build-status-droid-xamarin-development]:https://build.mobile.azure.com/v0.1/apps/596b9007-8b42-4325-9a49-0251fa24cc87/branches/xamarin-development/badge

[app-data-build-status-ios-xamarin-development]:https://build.mobile.azure.com/v0.1/apps/346c8576-7ceb-4b6b-8951-8846b42fdc68/branches/xamarin-development/badge

## Cognitive Locator

The Cognitive Locator project, known publicly as 'Busca.me', is a project dedicated to reporting and finding missing persons. The project was founded due to the disasters related to the earthquake of September 19, 2017, which affected multiple states in Mexico. At first, the project was solely focused on finding or reporting people who went missing as a result of the earthquake. However, now that the project has grown, it not only aims to support the people affected at that time, but to anyone who is going through this devastating situation.

Cognitive Locator is a project hoping that every person can potentially be a medium to report and locate people who have been publicly declared as 'missing'.

The source code of the Cognitive Locator project is under the MIT license, nonetheless, the ‘Busca.me’ team is in charge of the implementation of the project.

This project is non-profit, seeing how it only supports non-governmental organizations that face different cases of disappearances every day.

## Architecture

Cognitive Locator is based on Xamarin Forms and Azure platform: Azure Functions, CosmosDB, Storage, Notification Hub, Cognitive Services (Face API).

<img src="http://rcervantes.me/images/cognitive-locator-architecture.png" width="500">

## Setup project

#### Clone the project from GitHub repo

`git clone https://github.com/rcervantes/cognitive-locator.git`

#### Configure Azure resources

For this project is required:

- Azure Storage resource.
- Cosmos DB (Document DB).
- Cognitive Services (Face API).
- Azure Function App.
- Notification Hub.

<img src="http://rcervantes.me/images/cognitive-locator-resources.png">

To run locally the Azure Functions project we need to configure the local settings file, let's go and create a local.settings.json file with the following information:

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "AZURE_STORAGE_CONNECTION_STRING",
    "AzureWebJobsDashboard": "AZURE_STORAGE_CONNECTION_STRING",
    "Vision_API_Subscription_Key": "FACE_API_KEY",
    "Vision_API_PersonGroupId": "missingpeople",
    "Vision_API_Zone": "FACE_API_ZONE(e.g. westus or southcentralus)",
    "Vision_API_FaceList": "list",
    "CosmosDB_URI": "COSMOSDB_URI",
    "CosmosDB_AuthKey": "COSMOSDB_KEY",
    "CosmosDB_DatabaseId": "CognitiveLocator",
    "CosmosDB_PersonCollection": "Person",
    "NotificationHub_Access_Signature": "NOTIFICATION_HUB_CONNECTION_STRING",
    "NotificationHub_Name": "NOTIFICTION_HUB_NAME",
    "Cryptography_Key": "CRYPT_KEY",
    "MobileCenterID_Android": "MOBILECENTER_ANDROID_APP_ID",
    "MobileCenterID_iOS": "MOBILECENTER_IOS_APP_ID",
    "ImageStorageUrl": "https://YOUR_STORAGE_ACCOUNT.blob.core.windows.net/images/"
  }
}
```

#### Adding Authentication / Authorization

Once we have created our Azure Functions and correctly deployed it's time to configure some settings to enable Facebook authentication, select Authentication/Authorization under Networking.

<img src="http://rcervantes.me/images/cognitive-locator-functions-settings.png" width="800">

Turn on the 'App Service Authentication' feature, then select 'Action to take when request is not authenticated' to Log in with Facebook.

<img src="http://rcervantes.me/images/cognitive-locator-functions-add-facebook.png" width="500">

Click on Facebook option then set the Facebook Application Id, Application Secret and the scope: public_profile and email, then save the configuration. 

<img src="http://rcervantes.me/images/cognitive-locator-functions-add-facebook-settings.png" width="500">

Now enable advanced token and set an allowed external redirect URL. e.g. cognitivelocator://easyauth.callback

<img src="http://rcervantes.me/images/cognitive-locator-functions-add-facebook2.png" width="500">

#### Configure Facebook Application

Go to [Facebook Developer Portal](https://developer.facebook.com) with your Facebook account add a new application e.g. Locator. now you will be able to see your Dashboard with your Application Name, Application ID and Application Secret.

<img src="http://rcervantes.me/images/cognitive-locator-facebook-dashboard.png" width="500">

Now it's time to configure the login, add the valid OAuth redirect URIs using your Azure Function callback URI previously configured: **https://YOUR_AZURE_FUNCTION.azurewebsites.net/.auth/login/facebook/callback** and verify the reset client OAuth settings.

<img src="http://rcervantes.me/images/cognitive-locator-facebook-login-settings.png" width="800">

Now go to Settings -> Basic and configure your Android and iOS App.

For Android. Fill the required fields and verify the rest of the settings, if you need a key hash, you can get it using [this steps](https://blog.xamarin.com/simplified-android-keystore-signature-disovery/).

<img src="http://rcervantes.me/images/cognitive-locator-facebook-android-setting.png" width="500">

For iOS. Fill the required fields and verify the rest of the settings.

<img src="http://rcervantes.me/images/cognitive-locator-facebook-ios-setting.png" width="500">

At this moment you have successfuly configure your Facebook application to login with your apps.

#### Configure Face API

Now it's time to create our Face API 'Person Group' and 'Face List' in the specific API testing console:

**Person Group ID Parameters**
- personGroupId: missingpeople
- Content-Type: application/json
- Ocp-Apim-Subscription-Key: FACE_API_KEY
- Zone: Select the same zone where your FACE API has been created
- Post parameters:
```json
{
    "name":"Missing People",
    "userData":"Missing people person group"
}
```
- Link: [Person Group - Create a Person Group](https://southcentralus.dev.cognitive.microsoft.com/docs/services/563879b61984550e40cbbe8d/operations/563879b61984550f30395244)

**Face List Parameters**
- faceListId: list
- Content-Type: application/json
- Ocp-Apim-Subscription-Key: FACE_API_KEY
- Zone: Select the same zone where your FACE API has been created
- Link: [Face List - Create a Face List](https://southcentralus.dev.cognitive.microsoft.com/docs/services/563879b61984550e40cbbe8d/operations/563879b61984550f3039524b)

Now we are ready to run our backend locally.

<img src="http://rcervantes.me/images/cognitive-locator-backend.png">

With the testing console we are able to debug and see how Azure Functions operates and process each person that will be registered or processed for verification, after the application has been tested and validated it's time to deploy to Azure.

**Remember all local settings must be replicated on the Azure Function to work properly**

<img src="http://rcervantes.me/images/cognitive-locator-publish.png" width="450">

#### Mobile application (Xamarin)

Now it's time to configure our Xamarin application to point to our function.

In your CognitiveLocator\Settings.cs file set the following attributes:

- FunctionURL
- CryptographyKey
- FacebookAppId
- FacebookAppName

```csharp
namespace CognitiveLocator
{
    public class Settings
    {
#if DEBUG
        public const string FunctionURL = "https://YOUR_AZURE_FUNCTION.azurewebsites.net";
#else
        public const string FunctionURL = "https://YOUR_AZURE_FUNCTION.azurewebsites.net";
#endif

        public static string CognitiveLocator = "CognitiveLocator";
        public static string Language = "en-US";

        public static string CryptographyKey = "YOUR_CRYPT_KEY";
        public static string AzureWebJobsStorage = string.Empty;
        public static string MobileCenterID_Android = string.Empty;
        public static string MobileCenterID_iOS = string.Empty;
        public static string NotificationAccessSignature = string.Empty;
        public static string NotificationHubName = string.Empty;
        public static string ImageStorageUrl = string.Empty;

        public static string FacebookAppId = "YOUR_FACEBOOK_APP_ID";
        public static string FacebookAppName = "YOUR_FACEBOOK_APP_NAME";
        public static string MobileServiceAuthenticationToken = string.Empty;
        public static FacebookProfileData FacebookProfile = new FacebookProfileData();
    }
}
```

In your CognitiveLocator.Droid project go to Resources\values\strings.xml and set the following attributes:

```xml
<?xml version="1.0" encoding="utf-8"?>
<resources>
    <string name="app_id">YOUR_FACEBOOK_APP_ID</string>
    <string name="app_name">YOUR_FACEBOOK_APP_NAME</string>
</resources>
```
In addition go to AndroidManifest.xml and verify that your android:scheme and android:host are the same configured previously in our Azure Function authentication settings.

```xml
<activity android:name="com.microsoft.windowsazure.mobileservices.authentication.RedirectUrlActivity" android:launchMode="singleTop" android:noHistory="true">
    <intent-filter>
        <action android:name="android.intent.action.VIEW" />
        <category android:name="android.intent.category.DEFAULT" />
        <category android:name="android.intent.category.BROWSABLE" />
        <data android:scheme="cognitivelocator" android:host="easyauth.callback" />
    </intent-filter>
</activity>
```

In your CognitiveLocator.iOS project edit your Info.plist and set the following attributes:

```xml
<key>FacebookAppID</key>
<string>YOUR_FACEBOOK_APP_ID</string>
<key>FacebookDisplayName</key>
<string>YOUR_FACEBOOK_APP_NAME</string>
```

Congrats if you successfully configure the app you can run it now!

<img src="http://rcervantes.me/images/cognitive-locator-app.png" width="300">

<img src="http://rcervantes.me/images/cognitive-locator-app2.png" width="300">

## Credits

I want to thank to [all contributors](https://github.com/rcervantes/cognitive-locator/graphs/contributors) who had participated in this project and those people who still continue participating actively on this project.
