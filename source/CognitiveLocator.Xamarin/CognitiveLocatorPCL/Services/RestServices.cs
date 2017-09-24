using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using CognitiveLocator.Models.ApiModels;
using CognitiveLocator.Services;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using CognitiveLocator.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

[assembly: Dependency(typeof(RestServices))]
namespace CognitiveLocator.Services
{
    public class RestServices : IRestServices
    {
        protected const string BaseURL = "http://apisave.azurewebsites.net/";

        public async Task<bool> CreateReportAsync(CreateReportModel model, byte[] photo)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (var form = new MultipartFormDataContent())
                    {
                        var request = new HttpRequestMessage();
                        request.Headers.TryAddWithoutValidation("content-type", "multipart/form-data");
                        var imageContent = new ByteArrayContent(photo);
                        imageContent.Headers.TryAddWithoutValidation("content-type", "image/jpeg"); //= new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                                                                                                    //content1.Add(imageContent,"photo",$"{Guid.NewGuid().ToString()}.jpg");
                                                                                                    //request.Content = imageContent;
                        request.Method = HttpMethod.Post;
                        request.RequestUri = new Uri(BaseURL + "api/Photo/Post?"+ model.UrlFormat());

                        HttpContent content = null;

                        content = new ByteArrayContent(photo);
                        content.Headers.TryAddWithoutValidation("content-type", "image/jpeg");
                        content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                        {
                            Name = "photo",
                            FileName = $"{Guid.NewGuid().ToString()}.jpg"
                        };
                        form.Add(content);

                        request.Content = form;
                        using (var response = await client.SendAsync(request))
                        {
                            response.EnsureSuccessStatusCode();
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException?.ToString());
                return false;
            }
        }

        public async Task<List<Person>> SearchPersonByNameAsync(Person person)
        {
            return await SearchPersonByAsync($"ByName?name={person.Name}");
        }

        public async Task<List<Person>> SearchPersonByLastNameAsync(Person person)
        {
            return await SearchPersonByAsync($"ByLastName?lastName={person.LastName}");
        }

        public async Task<List<Person>> SearchPersonByPhotoAsync(byte[] photo)
        {
            throw new NotImplementedException();
        }

        async Task<List<Person>> SearchPersonByAsync(string endpoint)
        {
            List<Person> results = new List<Person>();
            try
            {
                using (var client = new HttpClient())
                {
                    var uri = $"{BaseURL}api/Find/{endpoint}";
                    using (var response = await client.PostAsync(uri, null))
                    {
                        response.EnsureSuccessStatusCode();

                        var json = await response.Content.ReadAsStringAsync();
                        results = JsonConvert.DeserializeObject<List<Person>>(json);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine((ex?.InnerException?.ToString()));
                return null;
            }

            return results;
        }
    }
}