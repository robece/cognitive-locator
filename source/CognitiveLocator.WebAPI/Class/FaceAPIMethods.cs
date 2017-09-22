using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
        public bool AddNewMissingPeople(String personName)
        {
            bool saved = false;
            return saved;
        }

        /// <summary>
        /// https://westus.dev.cognitive.microsoft.com/docs/services/563879b61984550e40cbbe8d/operations/563879b61984550f3039523b
        /// </summary>
        /// <param name="stgUrl"></param>
        /// <param name="personId"></param>
        /// <returns>Este metodo retorna un FaceId para guardar en la base de datos</returns>
        public bool AddFaceToMissingPeople(String stgUrl, String personId)
        {
            bool saved = false;
            return saved;
        }
    }
}