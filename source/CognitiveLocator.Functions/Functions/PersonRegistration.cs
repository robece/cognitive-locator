using CognitiveLocator.Domain;
using CognitiveLocator.Functions.Client;
using CognitiveLocator.Functions.Helpers;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CognitiveLocator.Functions
{
    public static class PersonRegistration
    {
        private static FaceClient client_face = new FaceClient();
        private static DocumentClient client_document = new DocumentClient(new Uri(Settings.DocumentDB), Settings.DocumentDBAuthKey);

        [FunctionName(nameof(PersonRegistration))]
        public static async Task Run(
            [BlobTrigger("images/{name}.{extension}")]CloudBlockBlob blob, string name, string extension, TraceWriter log)
        {
            //get json file from storage
            Person p = new Person();
            string json = string.Empty;
            CloudBlockBlob cbb = await StorageHelper.GetBlockBlob($"{name}.json", Settings.AzureWebJobsStorage, "metadata", false);

            //determine if image has a face
            List<JObject> list = await client_face.DetectFaces(blob.Uri.AbsoluteUri);

            //validate image extension
            if (extension != "jpg")
            {
                log.Info($"no valid extension for: {name}.{extension}");
                await blob.DeleteAsync();
                await cbb.DeleteAsync();
                return;
            }

            //if image has no faces
            if (list.Count == 0)
            {
                log.Info($"there are no faces in the image: {name}.{extension}");
                await blob.DeleteAsync();
                await cbb.DeleteAsync();
                return;
            }

            //if image has more than one face
            if (list.Count > 1)
            {
                log.Info($"multiple faces detected in the image: {name}.{extension}");
                await blob.DeleteAsync();
                await cbb.DeleteAsync();
                return;
            }

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    cbb.DownloadToStream(memoryStream);
                    json = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
                    p = JsonConvert.DeserializeObject<Person>(json);
                }

                //register person in Face API
                CreatePerson resultCreatePerson = await client_face.AddPersonToGroup(p.Name + " " + p.Lastname);
                AddPersonFace resultPersonFace = await client_face.AddPersonFace(blob.Uri.AbsoluteUri, resultCreatePerson.personId);
                AddFaceToList resultFaceToList = await client_face.AddFaceToList(blob.Uri.AbsoluteUri);

                //Add custom settings
                p.IsActive = 1;
                p.IsFound = 0;
                p.FaceAPIFaceId = resultFaceToList.persistedFaceId;
                p.FaceAPIPersonId = resultCreatePerson.personId;
                p.Picture = $"{name}.jpg";

                await client_document.CreateDatabaseIfNotExistsAsync(new Database { Id = Settings.DatabaseId });
                var collection = await client_document.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(Settings.DatabaseId), new DocumentCollection { Id = Settings.PersonCollectionId }, new RequestOptions { OfferThroughput = 1000 });
                var result = await client_document.CreateDocumentAsync(collection.Resource.SelfLink, p);
                var document = result.Resource;
            }
            catch (Exception ex)
            {
                await blob.DeleteAsync();
                await cbb.DeleteAsync();
                log.Info($"Error in file: {name}.{extension} - {ex.Message}");
                return;
            }

<<<<<<< HEAD
=======
            await cbb.DeleteAsync();
>>>>>>> master
            log.Info("person registered successfully");
        }
    }
}