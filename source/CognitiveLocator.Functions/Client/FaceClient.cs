using CognitiveLocator.Functions.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveLocator.Functions.Client
{
    public class FaceClient
    {
        public FaceClient(FaceAPIConfiguration configuration)
        {
            this.FaceAPIKey = configuration.FaceAPIKey;
            this.PersonGroupId = configuration.PersonGroupId;
            this.Zone = configuration.Zone;
            this.FaceListId = configuration.FaceListId;
        }

        private string FaceAPIKey = string.Empty;
        private string PersonGroupId = string.Empty;
        private string Zone = string.Empty;
        private string FaceListId = string.Empty;

        public async Task<CreatePerson> AddPersonToGroup(String personName)
        {
            using (var client = new HttpClient())
            {
                var service = $"https://{Zone}.api.cognitive.microsoft.com/face/v1.0/persongroups/" + PersonGroupId + "/persons";
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", FaceAPIKey);
                byte[] byteData = Encoding.UTF8.GetBytes("{'name':'" + personName + "','userData':'Descripcion Ejemplo'}");
                using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var httpResponse = await client.PostAsync(service, content);

                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        CreatePerson result = JsonConvert.DeserializeObject<CreatePerson>(await httpResponse.Content.ReadAsStringAsync());
                        return result;
                    }
                }
            }
            return null;
        }

        public async Task<AddPersonFace> AddPersonFace(String urlStg, String personId)
        {
            using (var client = new HttpClient())
            {
                var service = $"https://{Zone}.api.cognitive.microsoft.com/face/v1.0/persongroups/" + PersonGroupId + "/persons/" + personId + "/persistedFaces";
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", FaceAPIKey);
                byte[] byteData = Encoding.UTF8.GetBytes("{'url':'" + urlStg + "'}");
                using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var httpResponse = await client.PostAsync(service, content);

                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        AddPersonFace result = JsonConvert.DeserializeObject<AddPersonFace>(await httpResponse.Content.ReadAsStringAsync());
                        return result;
                    }
                }
            }
            return null;
        }

        public async Task<AddFaceToList> AddFaceToList(String stgUrl)
        {
            using (var client = new HttpClient())
            {
                var service = $"https://{Zone}.api.cognitive.microsoft.com/face/v1.0/facelists/" + FaceListId + "/persistedFaces";
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", FaceAPIKey);
                byte[] byteData = Encoding.UTF8.GetBytes("{'url':'" + stgUrl + "'}");
                using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var httpResponse = await client.PostAsync(service, content);

                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        AddFaceToList result = JsonConvert.DeserializeObject<AddFaceToList>(await httpResponse.Content.ReadAsStringAsync());
                        return result;
                    }
                }
            }
            return null;
        }

        public async Task<List<JObject>> DetectFaces(String url)
        {
            using (var client = new HttpClient())
            {
                var service = $"https://{Zone}.api.cognitive.microsoft.com/face/v1.0/detect";
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", FaceAPIKey);
                byte[] byteData = Encoding.UTF8.GetBytes("{'url':'" + url + "'}");
                using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var httpResponse = await client.PostAsync(service, content);

                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        List<JObject> result = JsonConvert.DeserializeObject<List<JObject>>(await httpResponse.Content.ReadAsStringAsync());
                        return result;
                    }
                }
            }
            return null;
        }

        public async Task<List<FindSimilar>> FindSimilarFaces(String faceId)
        {
            using (var client = new HttpClient())
            {
                var service = $"https://{Zone}.api.cognitive.microsoft.com/face/v1.0/findsimilars";
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", FaceAPIKey);
                byte[] byteData = Encoding.UTF8.GetBytes("{'faceId':'" + faceId + "','faceListId':'" + FaceListId + "','maxNumOfCandidatesReturned':25,'mode':'matchPerson'}");
                using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var httpResponse = await client.PostAsync(service, content);

                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        List<FindSimilar> result = JsonConvert.DeserializeObject<List<FindSimilar>>(await httpResponse.Content.ReadAsStringAsync());
                        return result;
                    }
                }
            }
            return null;
        }

        public async Task<bool> DeletePerson(string personGroupId, string personId)
        {
            using (var client = new HttpClient())
            {
                var service = $"https://{Zone}.api.cognitive.microsoft.com/face/v1.0/persongroups/{personGroupId}/persons/{personId}?";
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", FaceAPIKey);
                var httpResponse = await client.DeleteAsync(service);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> DeleteFace(string personGroupId, string personId, string persistedFaceId)
        {
            using (var client = new HttpClient())
            {
                var service = $"https://{Zone}.api.cognitive.microsoft.com/face/v1.0/persongroups/{personGroupId}/persons/{personId}/persistedFaces/{persistedFaceId}?";
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", FaceAPIKey);
                var httpResponse = await client.DeleteAsync(service);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<List<PersonInGroupOfPerson>> ListOfPersonsInPersonGroup(string personGroupId)
        {
            using (var client = new HttpClient())
            {
                var service = $"https://{Zone}.api.cognitive.microsoft.com/face/v1.0/persongroups/{personGroupId}/persons?";
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", FaceAPIKey);
                var httpResponse = await client.GetAsync(service);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    List<PersonInGroupOfPerson> result = JsonConvert.DeserializeObject<List<PersonInGroupOfPerson>>(await httpResponse.Content.ReadAsStringAsync());
                    return result;
                }
            }
            return null;
        }
    }
}