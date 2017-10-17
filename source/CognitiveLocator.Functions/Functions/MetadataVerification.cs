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
        private static DocumentClient client_document = new DocumentClient(new Uri(Settings.DocumentDB), Settings.DocumentDBAuthKey);

        [FunctionName(nameof(MetadataVerification))]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "MetadataVerification/")]HttpRequestMessage req, TraceWriter log)
        {
            Domain.MetadataVerification metadata = await req.Content.ReadAsAsync<Domain.MetadataVerification>();

            if (metadata == null)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, "Metadata is required to perform the search");
            }

            string query_attributes = string.Empty;

            if (!string.IsNullOrEmpty(metadata.Country))
                query_attributes += $"CONTAINS(UPPER(p.country), UPPER('{metadata.Country}')) AND ";

            if (!string.IsNullOrEmpty(metadata.Name))
                query_attributes += $"CONTAINS(UPPER(p.name), UPPER('{metadata.Name}')) AND ";

            if (!string.IsNullOrEmpty(metadata.Lastname))
                query_attributes += $"CONTAINS(UPPER(p.lastname), UPPER('{metadata.Lastname}')) AND ";

            if (!string.IsNullOrEmpty(metadata.Location))
                query_attributes += $"CONTAINS(UPPER(p.location), UPPER('{metadata.Location}')) AND ";

            if (!string.IsNullOrEmpty(metadata.Alias))
                query_attributes += $"CONTAINS(UPPER(p.alias), UPPER('{metadata.Alias}')) AND ";

            if (!string.IsNullOrEmpty(metadata.ReportedBy))
                query_attributes += $"CONTAINS(UPPER(p.reported_by), UPPER('{metadata.ReportedBy}')) AND ";

            if (query_attributes.Length > 0)
                query_attributes = query_attributes.Remove(query_attributes.Length - 4);

            List<Person> personsInDocuments = null;
            var collection = await client_document.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(Settings.DatabaseId), new DocumentCollection { Id = Settings.CollectionId }, new RequestOptions { OfferThroughput = 1000 });
            var query = client_document.CreateDocumentQuery<Person>(collection.Resource.SelfLink, new SqlQuerySpec()
            {
                QueryText = $"SELECT * FROM Person p WHERE {query_attributes}"
            });

            personsInDocuments = query.ToList();

            // Fetching the name from the path parameter in the request URL
            return req.CreateResponse(HttpStatusCode.OK, personsInDocuments);
        }
    }
}