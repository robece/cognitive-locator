using CognitiveLocator.Functions.Client;
using CognitiveLocator.Functions.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CognitiveLocator.Functions
{
    public static class FaceAPICleaner
    {
        [FunctionName("FaceAPICleaner")]
        public static async Task Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, TraceWriter log)
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

            //Search in Face API all persons in the group.
            List<PersonInGroupOfPerson> personsInFaceAPI = await client.ListOfPersonsInPersonGroup(configuration.PersonGroupId);

            //Search in documents all persons registered.
            List<Person> personsInDocuments = null;
            using (var document_client = new DocumentClient(new Uri(documentDB), documentDBAuthKey))
            {
                var collection = await document_client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(DatabaseId), new DocumentCollection { Id = CollectionId }, new RequestOptions { OfferThroughput = 1000 });

                var query = document_client.CreateDocumentQuery<Person>(collection.Resource.SelfLink, new SqlQuerySpec()
                {
                    QueryText = "SELECT * FROM Person f"
                });

                personsInDocuments = query.ToList();
            }

            /* Search persons in Face API, check if exists in documents, if not then deleted faces and persons from Face API */

            await Task.Run(() =>
            {
                Parallel.ForEach(personsInFaceAPI, async person =>
                {
                    //search person id from face api in documents.
                    Person person_in_document_and_face_api = personsInDocuments.Find(x => x.FaceAPI_PersonId == person.PersonId);

                    //if person registered in Face API not exists in documents then delete it from Face API.
                    if (person_in_document_and_face_api == null)
                    {
                        if (Parallel.ForEach(person.PersistedFaceIds, async persistedFaceId =>
                        {
                            bool result = await client.DeleteFace(configuration.PersonGroupId, person.PersonId, persistedFaceId);
                        }).IsCompleted)
                        {
                            bool result = await client.DeletePerson(configuration.PersonGroupId, person.PersonId);
                        };
                    }
                });
            });
        }
    }
}