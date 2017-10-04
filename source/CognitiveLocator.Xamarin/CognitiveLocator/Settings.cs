using System;
namespace CognitiveLocator
{
    public class Settings
    {
        public static string MobileCenterID_Android = "f8963ef0-4239-46ea-b69c-621fafe09d0c";
        public static string MobileCenterID_iOS = "0da75977-ccf2-43fd-ba88-ae712f9a3568";

#if DEBUG
        public const string BaseURL = "https://cognitivelocatordev.azurewebsites.net/";
#else
        public const string BaseURL = "https://cognitivelocator.azurewebsites.net/";
#endif

    }
}
