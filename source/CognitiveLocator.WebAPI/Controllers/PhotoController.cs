using CognitiveLocator.WebAPI.Class;
using CognitiveLocator.WebAPI.Models;
using CognitiveLocator.WebAPI.Models.FaceApiModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
        public async Task<IHttpActionResult> PostFormData([FromUri] int IsFound, string Name, string LastName, string Alias="", string Location="", string Notes="", string BirthDate = "", string ReportedBy="")
        {
            byte[] fileBytes = null;
            String FileName = string.Empty;
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            //in case of dealing in local change  D/local/Temp by App-Data
            string root = "D:/local/Temp/"; // HttpContext.Current.Server.MapPath("~/App_Data");
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
                //AddPersonToGroup
                FaceAPIMethods ObjFaceApiPerson = new FaceAPIMethods();
                //AddPersonId
                CreatePerson resultCreatePerson = await ObjFaceApiPerson.AddPersonToGroup(Name + " " + LastName);
                //AddFace
                AddPersonFace resultPersonFace = await ObjFaceApiPerson.AddPersonFace(uri, resultCreatePerson.personId);
                //SaveDB
                AddFaceToList resultFaceToList = await ObjFaceApiPerson.AddFaceToList(uri);
                Nullable<DateTime> dateNull = null;
                Person person = new Person()
                {
                    Alias = Alias,
                    FaceId = resultFaceToList.persistedFaceId,
                    IdPerson = resultCreatePerson.personId,
                    Picture = uri,
                    IsActive = 1,
                    IsFound = IsFound,
                    LastName = LastName,
                    Name = Name,
                    Location = Location,
                    Notes = Notes,
                    ReportedBy = ReportedBy,
                    BirthDate = (string.IsNullOrEmpty(BirthDate)) ? dateNull : DateTime.ParseExact(BirthDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                };
                await new SPQuery().AddPersonNotFound(person);

                File.Delete(provider.FileData.First().LocalFileName);
                return Ok();
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}
