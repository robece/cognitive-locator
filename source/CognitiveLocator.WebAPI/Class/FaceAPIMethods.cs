using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace CognitiveLocator.WebAPI.Class
{
    public class FaceAPIMethods
    {
        private static string FaceAPIKey = ConfigurationManager.AppSettings["FaceAPIKey"].ToString();
        private static string PersonGroupId = ConfigurationManager.AppSettings["PersonGroupId"].ToString();
        private static string Zone = ConfigurationManager.AppSettings["Zone"].ToString();
        /// <summary>
        /// https://westus.dev.cognitive.microsoft.com/docs/services/563879b61984550e40cbbe8d/operations/563879b61984550f3039523c
        /// </summary>
        /// <param name="personName"></param>
        /// <returns>Este metodo retorna un personId el cual debera ser almacenado en la base de datos</returns>
        public async System.Threading.Tasks.Task<bool> AddNewMissingPeopleAsync(String personName)
        {
            bool saved = false;
            var client = new HttpClient();
            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", FaceAPIKey);
            var uri = "https://westus.api.cognitive.microsoft.com/face/v1.0/persongroups/"+PersonGroupId+"/persons";
            HttpResponseMessage response;
            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("{'name':'"+personName+"','userData':'Descripcion Ejemplo'}");
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
            }
            return saved;
        }

        /// <summary>
        /// https://westus.dev.cognitive.microsoft.com/docs/services/563879b61984550e40cbbe8d/operations/563879b61984550f3039523b
        /// </summary>
        /// <param name="stgUrl"></param>
        /// <param name="personId"></param>
        /// <returns>Este metodo retorna un FaceId para guardar en la base de datos</returns>
        public async System.Threading.Tasks.Task<bool> AddFaceToMissingPeopleAsync(String stgUrl, String personId)
        {
            bool saved = false;
            var client = new HttpClient();
            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", FaceAPIKey);
            // Request parameters
            var uri = "https://westus.api.cognitive.microsoft.com/face/v1.0/persongroups/"+PersonGroupId+"/persons/"+personId+"/persistedFaces?";
            HttpResponseMessage response;
            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("{'url':'"+stgUrl+"'}");
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json >");
                response = await client.PostAsync(uri, content);
            }

            return saved;
        }
    }
}