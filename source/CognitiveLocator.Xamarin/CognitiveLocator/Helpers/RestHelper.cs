using CognitiveLocator.Domain;
using CognitiveLocator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using CognitiveLocator.Requests;

namespace CognitiveLocator.Helpers
{
    public class RestHelper
    {
        public static async Task<Dictionary<string, string>> GetMobileSettings()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("x-zumo-auth", Settings.MobileServiceAuthenticationToken);

                var service = $"{Settings.FunctionURL}/api/MobileSettings/";

                byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                byte[] key = Guid.NewGuid().ToByteArray();
                var token = Convert.ToBase64String(time.Concat(key).ToArray());
                token = DependencyService.Get<ISecurityService>().Encrypt(token, Settings.Cryptography);

                MobileSettingsRequest request = new MobileSettingsRequest();
                request.Token = token;

                byte[] byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request));
                using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var httpResponse = await client.PostAsync(service, content);

                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        Dictionary<string, string> result = JsonConvert.DeserializeObject<Dictionary<string, string>>(await httpResponse.Content.ReadAsStringAsync());
                        return result;
                    }
                }
            }
            return null;
        }

        public static async Task<List<Person>> MetadataVerification(MetadataVerification metadata)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("x-zumo-auth", Settings.MobileServiceAuthenticationToken);

                var service = $"{Settings.FunctionURL}/api/MetadataVerification/";

                byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                byte[] key = Guid.NewGuid().ToByteArray();
                var token = Convert.ToBase64String(time.Concat(key).ToArray());
                token = DependencyService.Get<ISecurityService>().Encrypt(token, Settings.Cryptography);

                MetadataVerificationRequest request = new MetadataVerificationRequest();
                request.Token = token;
                request.Metadata = metadata;
              
                byte[] byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request));
                using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var httpResponse = await client.PostAsync(service, content);

                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        var str = await httpResponse.Content.ReadAsStringAsync();
                        List<Person> result = JsonConvert.DeserializeObject<List<Person>>(str);
                        return result;
                    }
                }
            }
            return null;
        }

        public static async Task<List<Person>> ImageVerification(string fileName)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("x-zumo-auth", Settings.MobileServiceAuthenticationToken);

                var service = $"{Settings.FunctionURL}/api/ImageVerification/";

                byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                byte[] key = Guid.NewGuid().ToByteArray();
                var token = Convert.ToBase64String(time.Concat(key).ToArray());
                token = DependencyService.Get<ISecurityService>().Encrypt(token, Settings.Cryptography);

                ImageVerificationRequest request = new ImageVerificationRequest();
                request.Token = token;
                request.ImageName = fileName;

                byte[] byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request));
                using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var httpResponse = client.PostAsync(service, content).Result;

                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        var str = await httpResponse.Content.ReadAsStringAsync();
                        List<Person> result = JsonConvert.DeserializeObject<List<Person>>(str);
                        return result;
                    }
                }
            }
            return null;
        }
    }
}