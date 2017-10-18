using CognitiveLocator.Domain;
using CognitiveLocator.Functions.Client;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CognitiveLocator.Functions
{
    public static class PersonVerification
    {
        private static FaceClient client_face = new FaceClient();
        private static DocumentClient client_document = new DocumentClient(new Uri(Settings.DocumentDB), Settings.DocumentDBAuthKey);

        [FunctionName(nameof(PersonVerification))]
        public static async Task Run(
            [BlobTrigger("verification/{name}.{extension}")]CloudBlockBlob blob, string name, string extension, TraceWriter log)
        {
            //determine if image has a face
            List<JObject> list = await client_face.DetectFaces(blob.Uri.AbsoluteUri);

            //validate image extension 
            if (extension != "jpg")
            {
                log.Info($"no valid extension for: {name}.{extension}");
                await blob.DeleteAsync();
                return;
            }

            //if image has no faces
            if (list.Count == 0)
            {
                log.Info($"there are no faces in the image: {name}.jpg");
                await blob.DeleteAsync();
                return;
            }

            //if image has more than one face
            if (list.Count > 1)
            {
                log.Info($"multiple faces detected in the image: {name}.jpg");
                await blob.DeleteAsync();
                return;
            }

            //verificate person in Face API
            string detectFaceId = list.First()["faceId"].ToString();
            List<FindSimilar> similarFaces = await client_face.FindSimilarFaces(detectFaceId);

            List<Person> list_persons = new List<Person>();
            foreach (var i in similarFaces)
            {
                var collection = await client_document.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(Settings.DatabaseId), new DocumentCollection { Id = Settings.CollectionId }, new RequestOptions { OfferThroughput = 1000 });
                var query = client_document.CreateDocumentQuery<Person>(collection.Resource.SelfLink, new SqlQuerySpec()
                {
                    QueryText = "SELECT * FROM Person p WHERE (p.faceapi_faceid = @id)",
                    Parameters = new SqlParameterCollection()
                    {
                        new SqlParameter("@id", i.persistedFaceId)
                    }
                });

                List<Person> personsInDocuments = query.ToList();
                if (personsInDocuments.Count != 0)
                {
                    foreach (Person pe in personsInDocuments)
                        list_persons.Add(pe);
                }
            }

            await blob.DeleteAsync();
            log.Info("person verified successfully");
        }
    }
}