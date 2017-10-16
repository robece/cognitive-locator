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

namespace CognitiveLocator.Functions
{
    public static class MetadataVerification
    {
        [FunctionName("MetadataVerification")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "MetadataVerification/")]HttpRequestMessage req, TraceWriter log)
        {
            var documentDB = Environment.GetEnvironmentVariable("CosmosDB_URI");
            var documentDBAuthKey = Environment.GetEnvironmentVariable("CosmosDB_AuthKey");
            var databaseId = Environment.GetEnvironmentVariable("CosmosDB_DatabaseId");
            var collectionId = Environment.GetEnvironmentVariable("CosmosDB_Collection");

            Domain.MetadataVerification metadata = await req.Content.ReadAsAsync<Domain.MetadataVerification>();

            if (metadata == null)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, "Metadata is required to perform the search");
            }

            string query_attributes = string.Empty;

            if (!string.IsNullOrEmpty(metadata.Country))
                query_attributes += $"CONTAINS(UPPER(p._country), UPPER('{metadata.Country}')) AND ";

            if (!string.IsNullOrEmpty(metadata.Name))
                query_attributes += $"CONTAINS(UPPER(p._name), UPPER('{metadata.Name}')) AND ";

            if (!string.IsNullOrEmpty(metadata.Lastname))
                query_attributes += $"CONTAINS(UPPER(p._lastname), UPPER('{metadata.Lastname}')) AND ";

            if (!string.IsNullOrEmpty(metadata.Location))
                query_attributes += $"CONTAINS(UPPER(p._location), UPPER('{metadata.Location}')) AND ";

            if (!string.IsNullOrEmpty(metadata.Alias))
                query_attributes += $"CONTAINS(UPPER(p._alias), UPPER('{metadata.Alias}')) AND ";

            if (!string.IsNullOrEmpty(metadata.ReportedBy))
                query_attributes += $"CONTAINS(UPPER(p._reported_by), UPPER('{metadata.ReportedBy}')) AND ";

            if (query_attributes.Length > 0)
                query_attributes = query_attributes.Remove(query_attributes.Length - 4);

            List<Person> personsInDocuments = null;
            using (var document_client = new DocumentClient(new Uri(documentDB), documentDBAuthKey))
            {
                var collection = await document_client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(databaseId), new DocumentCollection { Id = collectionId }, new RequestOptions { OfferThroughput = 1000 });

                var query = document_client.CreateDocumentQuery<Person>(collection.Resource.SelfLink, new SqlQuerySpec()
                {
                    QueryText = $"SELECT * FROM Person p WHERE {query_attributes}"
                });

                personsInDocuments = query.ToList();
            }

            // Fetching the name from the path parameter in the request URL
            return req.CreateResponse(HttpStatusCode.OK, personsInDocuments);
        }
    }
}