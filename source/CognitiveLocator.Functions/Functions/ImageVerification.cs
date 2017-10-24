using CognitiveLocator.Functions.Helpers;
using CognitiveLocator.Domain;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json.Linq;
using CognitiveLocator.Functions.Client;

namespace CognitiveLocator.Functions
{
    public static class ImageVerification
    {
        private static FaceClient client_face = new FaceClient();
        private static DocumentClient client_document = new DocumentClient(new Uri(Settings.DocumentDB), Settings.DocumentDBAuthKey);

        [FunctionName(nameof(ImageVerification))]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "ImageVerification/")]HttpRequestMessage req, TraceWriter log)
        {
            Domain.ImageVerificationRequest request = await req.Content.ReadAsAsync<Domain.ImageVerificationRequest>();

            var decrypted_token = SecurityHelper.Decrypt(request.Token, Settings.CryptographyKey);

            byte[] data = Convert.FromBase64String(decrypted_token);
            DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
            if (when < DateTime.UtcNow.AddMinutes(-5))
            {
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            if (string.IsNullOrEmpty(request.ImageName))
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, "Image is required to perform the search");
            }

            //get json file from storage
            CloudBlockBlob blobImage = await StorageHelper.GetBlockBlob(request.ImageName, Settings.AzureWebJobsStorage, "verification", false);

            List<Person> result = new List<Person>();
            if (blobImage.Exists())
            {
                //determine if image has a face
                List<JObject> list = await client_face.DetectFaces(blobImage.Uri.AbsoluteUri);

                //validate image extension
                if (blobImage.Properties.ContentType != "image/jpeg")
                {
                    log.Info($"no valid content type for: {blobImage.Name}");
                    await blobImage.DeleteAsync();
                    return req.CreateResponse(HttpStatusCode.BadRequest, result);
                }

                //if image has no faces
                if (list.Count == 0)
                {
                    log.Info($"there are no faces in the image: {blobImage.Name}");
                    await blobImage.DeleteAsync();
                    return req.CreateResponse(HttpStatusCode.BadRequest, result);
                }

                //if image has more than one face
                if (list.Count > 1)
                {
                    log.Info($"multiple faces detected in the image: {blobImage.Name}");
                    await blobImage.DeleteAsync();
                    return req.CreateResponse(HttpStatusCode.BadRequest, result);
                }

                //verificate person in Face API
                string detectFaceId = list.First()["faceId"].ToString();
                List<FindSimilar> similarFaces = await client_face.FindSimilarFaces(detectFaceId);

                foreach (var i in similarFaces)
                {
                    var collection = await client_document.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(Settings.DatabaseId), new DocumentCollection { Id = Settings.PersonCollectionId }, new RequestOptions { OfferThroughput = 1000 });
                    var query = client_document.CreateDocumentQuery<Person>(collection.Resource.SelfLink, new SqlQuerySpec()
                    {
                        QueryText = "SELECT * FROM Person p WHERE (p.faceapifaceid = @id)",
                        Parameters = new SqlParameterCollection()
                    {
                        new SqlParameter("@id", i.persistedFaceId)
                    }
                    });

                    List<Person> personsInDocuments = query.ToList();
                    if (personsInDocuments.Count != 0)
                    {
                        foreach (Person pe in personsInDocuments)
                            result.Add(pe);
                    }
                }
            }

            await blobImage.DeleteAsync();
            log.Info("person verified successfully");

            // Fetching the name from the path parameter in the request URL
            return req.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}