using System;
using Xamarin.Forms;
using System.Globalization;
using CognitiveLocator.iOS.Services;
using CognitiveLocator.Interfaces;
using Microsoft.WindowsAzure.MobileServices;
using UIKit;
using Foundation;
using Newtonsoft.Json.Linq;
using Facebook.LoginKit;
using Facebook.CoreKit;
using Newtonsoft.Json;
using CognitiveLocator.Domain;
using System.Threading.Tasks;

[assembly: Dependency(typeof(AuthenticateService))]
namespace CognitiveLocator.iOS.Services
{
    public class AuthenticateService : IAuthenticate
    {
        private MobileServiceUser user;

        public void GetUserInfo()
        {
            var request = new GraphRequest("/me", null, AccessToken.CurrentAccessToken.TokenString, string.Empty, "GET");
            var requestConnection = new GraphRequestConnection();
            requestConnection.AddRequest(request, (connection, result, error) =>
            {
                Settings.FacebookProfile.id = result.ValueForKeyPath(new NSString("id")).ToString();
                Settings.FacebookProfile.name = result.ValueForKeyPath(new NSString("name")).ToString();
                Xamarin.Forms.MessagingCenter.Send(new object(), "connected");
            });
            requestConnection.Start();
        }

        public string GetToken()
        {
            return AccessToken.CurrentAccessToken.TokenString;
        }

        public bool IsAuthenticated()
        {
            return (AccessToken.CurrentAccessToken == null) ? false : true;
        }

        public async Task<string> Authenticate(JObject token)
        {
            var result = string.Empty;
            try
            {
                user = await AppDelegate.MobileClient.LoginAsync(MobileServiceAuthenticationProvider.Facebook, token);
                result = user.MobileServiceAuthenticationToken;
            }
            catch
            {
                result = string.Empty;
            }
            return result;
        }

        public async System.Threading.Tasks.Task ClearToken()
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                foreach (var cookie in NSHttpCookieStorage.SharedStorage.Cookies)
                {
                    NSHttpCookieStorage.SharedStorage.DeleteCookie(cookie);
                }

                var manager = new LoginManager();
                manager.LogOut();

                AppDelegate.MobileClient.LogoutAsync();
            });
        }
    }
}