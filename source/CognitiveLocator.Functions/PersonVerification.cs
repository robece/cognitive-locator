using CognitiveLocator.Functions.Client;
using CognitiveLocator.Functions.Models;
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
        [FunctionName("PersonVerification")]
        public static async Task Run(
            [BlobTrigger("verification/{name}.jpg")]CloudBlockBlob blob, string name, TraceWriter log)
        {
            FaceAPIConfiguration configuration = new FaceAPIConfiguration();
            configuration.FaceAPIKey = Environment.GetEnvironmentVariable("Vision_API_Subscription_Key");
            configuration.PersonGroupId = Environment.GetEnvironmentVariable("Vision_API_PersonGroupId");
            configuration.Zone = Environment.GetEnvironmentVariable("Vision_API_Zone");
            configuration.FaceListId = Environment.GetEnvironmentVariable("Vision_API_FaceList");
            var documentDB = Environment.GetEnvironmentVariable("CosmosDB_URI");
            var documentDBAuthKey = Environment.GetEnvironmentVariable("CosmosDB_AuthKey");
            var DatabaseId = Environment.GetEnvironmentVariable("CosmosDB_DatabaseId");
            var CollectionId = Environment.GetEnvironmentVariable("CosmosDB_Collection");

            FaceClient client = new FaceClient(configuration);

            //determine if image has a face
            List<JObject> list = await client.DetectFaces(blob.Uri.AbsoluteUri);

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
            List<FindSimilar> similarFaces = await client.FindSimilarFaces(detectFaceId);

            List<Person> list_persons = new List<Person>();
            foreach (var i in similarFaces)
            {
                using (var document_client = new DocumentClient(new Uri(documentDB), documentDBAuthKey))
                {
                    var collection = await document_client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(DatabaseId), new DocumentCollection { Id = CollectionId }, new RequestOptions { OfferThroughput = 1000 });

                    var query = document_client.CreateDocumentQuery<Person>(collection.Resource.SelfLink, new SqlQuerySpec()
                    {
                        QueryText = "SELECT * FROM Person p WHERE (p._faceapi_faceid = @id)",
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
            }

            await blob.DeleteAsync();
            log.Info("person verified successfully");
        }
    }
}