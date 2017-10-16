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
using System.Threading.Tasks;

namespace CognitiveLocator.Functions
{
    public static class PersonRegistration
    {
        [FunctionName("PersonRegistration")]
        public static async Task Run(
            [BlobTrigger("images/{name}.jpg")]CloudBlockBlob blob, string name, TraceWriter log)
        {
            FaceAPIConfiguration configuration = new FaceAPIConfiguration();
            configuration.FaceAPIKey = Environment.GetEnvironmentVariable("Vision_API_Subscription_Key");
            configuration.PersonGroupId = Environment.GetEnvironmentVariable("Vision_API_PersonGroupId");
            configuration.Zone = Environment.GetEnvironmentVariable("Vision_API_Zone");
            configuration.FaceListId = Environment.GetEnvironmentVariable("Vision_API_FaceList");
            var documentDB = Environment.GetEnvironmentVariable("CosmosDB_URI");
            var documentDBAuthKey = Environment.GetEnvironmentVariable("CosmosDB_AuthKey");
            var databaseId = Environment.GetEnvironmentVariable("CosmosDB_DatabaseId");
            var collectionId = Environment.GetEnvironmentVariable("CosmosDB_Collection");

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

            
            try
            {
                //register person in Face API
                CreatePerson resultCreatePerson = await client.AddPersonToGroup(blob.Metadata["name"] + " " + blob.Metadata["lastname"]);
                AddPersonFace resultPersonFace = await client.AddPersonFace(blob.Uri.AbsoluteUri, resultCreatePerson.personId);
                AddFaceToList resultFaceToList = await client.AddFaceToList(blob.Uri.AbsoluteUri);

                Person p = new Person();
                p.Name = blob.Metadata["name"];
                p.LastName = blob.Metadata["lastname"];
                p.Location = blob.Metadata["location"];
                p.Country = blob.Metadata["country"];
                p.Notes = blob.Metadata["notes"];
                p.Alias = blob.Metadata["alias"];
                p.BirthDate = blob.Metadata["birthdate"];
                p.ReportedBy = blob.Metadata["reportedby"];
                p.IsActive = 1;
                p.IsFound = 0;
                p.Picture = $"{name}.jpg";
                p.FaceAPI_FaceId = resultFaceToList.persistedFaceId;
                p.FaceAPI_PersonId = resultCreatePerson.personId;
                p.PendingToBeDeleted = false;

                using (var document_client = new DocumentClient(new Uri(documentDB), documentDBAuthKey))
                {
                    await document_client.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseId });
                    var collection = await document_client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(databaseId), new DocumentCollection { Id = collectionId }, new RequestOptions { OfferThroughput = 1000 });

                    var result = await document_client.CreateDocumentAsync(collection.Resource.SelfLink, p);
                    var document = result.Resource;
                }

            }
            catch (Exception ex)
            {
                await blob.DeleteAsync();
                log.Info($"Error in file: {name}.jpg - {ex.Message}");
                return;
            }
            
            log.Info("person registered successfully");
        }
    }
}