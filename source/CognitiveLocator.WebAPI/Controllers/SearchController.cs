using CognitiveLocator.WebAPI.Class;
using CognitiveLocator.WebAPI.Models.FaceApiModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CognitiveLocator.WebAPI.Controllers
{
    [RoutePrefix("api/Find")]
    public class SearchController : ApiController
    {
        [Route("ByFace")]
        public async Task<IHttpActionResult> ByFace()
        {
            byte[] fileBytes = null;
            String FileName = string.Empty;
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                var filesReadToProvider = await Request.Content.ReadAsMultipartAsync(provider);
                fileBytes = File.ReadAllBytes(provider.FileData.First().LocalFileName);
                foreach (MultipartFileData file in provider.FileData)
                {
                    FileName = file.Headers.ContentDisposition.FileName;
                }

                ClassStg storage = new ClassStg();
                //ModelPerson ObjPerson = new ModelPerson();
                string uri = await storage.UploadPhoto(new MemoryStream(fileBytes), FileName);
                FaceAPIMethods ObjFaceApiPerson = new FaceAPIMethods();
                List<JObject> detectResult = await ObjFaceApiPerson.DetectFace(uri);
                string detectFaceId = detectResult.First()["faceId"].ToString();
                FindSimilar similarFace = await ObjFaceApiPerson.FindSimilarFace(detectFaceId);
                File.Delete(provider.FileData.First().LocalFileName);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }
        [Route("ByName")]
        public async Task<IHttpActionResult> ByName([FromUri] string name,[FromUri] string lastName)
        {
            return Ok();
        }

    }
}
