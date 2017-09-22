using CognitiveLocator.WebAPI.Class;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CognitiveLocator.WebAPI.Controllers
{
    [RoutePrefix("api/Photo")]
    public class PhotoController : ApiController
    {
        [HttpPost]
        [Route("Post")]
        public async Task<HttpResponseMessage> PostFormData()
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
                string uri = await storage.UploadPhoto(new MemoryStream(fileBytes), FileName);
                File.Delete(provider.FileData.First().LocalFileName);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
