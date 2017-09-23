using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using CognitiveLocator.Models.ApiModels;
using CognitiveLocator.Services;
using Xamarin.Forms;
using System.Diagnostics;

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
    }
}
