using System;
using Xamarin.Forms;
using System.Threading;
using System.Globalization;
using CognitiveLocator.Droid.Services;
using CognitiveLocator.Interfaces;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using Xamarin.Facebook.Login;
using Xamarin.Facebook;
using Android.OS;
using CognitiveLocator.Droid.Callbacks;
using CognitiveLocator.Domain;
using Newtonsoft.Json;
using CognitiveLocator.ViewModels;
using CognitiveLocator.Pages;

[assembly: Dependency(typeof(AuthenticateService))]
namespace CognitiveLocator.Droid.Services
{
    public class AuthenticateService : IAuthenticate
    {
        private MobileServiceUser user;

        public async void GetUserInfo()
        {
            GraphCallback graphCallBack = new GraphCallback();
            graphCallBack.RequestCompleted -= GraphCallBack_RequestCompleted;
            graphCallBack.RequestCompleted += GraphCallBack_RequestCompleted;

            var request = new GraphRequest(AccessToken.CurrentAccessToken, "/me", null, HttpMethod.Get, graphCallBack);
            await Task.Run(() => { request.ExecuteAsync(); });
        }

        void GraphCallBack_RequestCompleted(object sender, Callbacks.GraphResponseEventArgs e)
        {
            Settings.FacebookProfile = JsonConvert.DeserializeObject<FacebookProfileData>(e.Response.JSONObject.ToString());
            Xamarin.Forms.MessagingCenter.Send(new object(),"connected");
        }

        public string GetToken()
        {
            return Xamarin.Facebook.AccessToken.CurrentAccessToken.Token;
        }

        public bool IsAuthenticated()
        {
            return (Xamarin.Facebook.AccessToken.CurrentAccessToken == null) ? false : true;
        }

        public async Task<string> Authenticate(JObject token)
        {
            var result = string.Empty;
            try
            {
                user = await MainActivity.MobileClient.LoginAsync(MobileServiceAuthenticationProvider.Facebook, token);
                result = user.MobileServiceAuthenticationToken;
            }
            catch
            {
                result = string.Empty;
            }
            return result;
        }

        public async Task ClearToken()
        {
            await Task.Run(async() =>
            {
                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop){
                    Android.Webkit.CookieManager.Instance.RemoveAllCookies(null);
                    Android.Webkit.CookieManager.Instance.RemoveSessionCookies(null);
                }
                else
                {
                    Android.Webkit.CookieManager.Instance.RemoveAllCookie();
                    Android.Webkit.CookieManager.Instance.Flush();
                }

                LoginManager.Instance.LogOut();
                await MainActivity.MobileClient.LogoutAsync();
            });
        }
    }
}