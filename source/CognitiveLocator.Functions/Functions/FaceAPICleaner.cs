using CognitiveLocator.Domain;
using CognitiveLocator.Functions.Client;
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
        private static FaceClient client_face = new FaceClient();
        private static DocumentClient client_document = new DocumentClient(new Uri(Settings.DocumentDB), Settings.DocumentDBAuthKey);

        [FunctionName(nameof(FaceAPICleaner))]
        public static async Task Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            //Search in Face API all persons in the group.
            List<PersonInGroupOfPerson> personsInFaceAPI = await client_face.ListOfPersonsInPersonGroup(Settings.PersonGroupId);

            //Search in documents all persons registered.
            List<Person> personsInDocuments = null;

            var collection = await client_document.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(Settings.DatabaseId), new DocumentCollection { Id = Settings.PersonCollectionId }, new RequestOptions { OfferThroughput = 1000 });
            var query = client_document.CreateDocumentQuery<Person>(collection.Resource.SelfLink, new SqlQuerySpec()
            {
                QueryText = "SELECT * FROM Person f"
            });

            personsInDocuments = query.ToList();

            /* Search persons in Face API, check if exists in documents, if not then deleted faces and persons from Face API */

            await Task.Run(() =>
            {
                Parallel.ForEach(personsInFaceAPI, async person =>
                {
                    //search person id from face api in documents.
                    Person person_in_document_and_face_api = personsInDocuments.Find(x => x.FaceAPIPersonId == person.PersonId);

                    //if person registered in Face API not exists in documents then delete it from Face API.
                    if (person_in_document_and_face_api == null)
                    {
                        if (Parallel.ForEach(person.PersistedFaceIds, async persistedFaceId =>
                        {
                            bool result = await client_face.DeleteFace(Settings.PersonGroupId, person.PersonId, persistedFaceId);
                        }).IsCompleted)
                        {
                            bool result = await client_face.DeletePerson(Settings.PersonGroupId, person.PersonId);
                        };
                    }
                });
            });
        }
    }
}