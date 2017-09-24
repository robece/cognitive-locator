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
                    using (var content = new MultipartFormDataContent())
                    {
                        content.Add(new StreamContent(new MemoryStream(photo)));

                        using (var response = await client.PostAsync(BaseURL + model.UrlFormat, content))
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
                using(var client = new HttpClient())
                {
                    var uri = $"{BaseURL}api/Find/{endpoint}";
                    using(var response = await client.PostAsync(uri, null))
                    {
                        response.EnsureSuccessStatusCode();

                        var json = await response.Content.ReadAsStringAsync();
                        results = JsonConvert.DeserializeObject<List<Person>>(json);
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine((ex?.InnerException?.ToString()));
                return null;
            }

            return results;
        }
    }
}
