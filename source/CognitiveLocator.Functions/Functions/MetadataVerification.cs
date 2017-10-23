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

namespace CognitiveLocator.Functions
{
    public static class MetadataVerification
    {
        private static DocumentClient client_document = new DocumentClient(new Uri(Settings.DocumentDB), Settings.DocumentDBAuthKey);

        [FunctionName(nameof(MetadataVerification))]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "MetadataVerification/")]HttpRequestMessage req, TraceWriter log)
        {
            Domain.MetadataVerificationRequest request = await req.Content.ReadAsAsync<Domain.MetadataVerificationRequest>();

            var decrypted_token = SecurityHelper.Decrypt(request.Token, Settings.CryptographyKey);

            byte[] data = Convert.FromBase64String(decrypted_token);
            DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
            if (when < DateTime.UtcNow.AddMinutes(-5))
            {
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            if (request.Metadata == null)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, "Metadata is required to perform the search");
            }

            string query_attributes = string.Empty;

            if (!string.IsNullOrEmpty(request.Metadata.Country))
                query_attributes += $"CONTAINS(UPPER(p.country), UPPER('{request.Metadata.Country}')) AND ";

            if (!string.IsNullOrEmpty(request.Metadata.Name))
                query_attributes += $"CONTAINS(UPPER(p.name), UPPER('{request.Metadata.Name}')) AND ";

            if (!string.IsNullOrEmpty(request.Metadata.Lastname))
                query_attributes += $"CONTAINS(UPPER(p.lastname), UPPER('{request.Metadata.Lastname}')) AND ";

            if (!string.IsNullOrEmpty(request.Metadata.ReportedBy))
                query_attributes += $"CONTAINS(UPPER(p.reportedby), UPPER('{request.Metadata.ReportedBy}')) AND ";

            if (query_attributes.Length > 0)
                query_attributes = query_attributes.Remove(query_attributes.Length - 4);

            List<Person> personsInDocuments = null;
            var collection = await client_document.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(Settings.DatabaseId), new DocumentCollection { Id = Settings.PersonCollectionId }, new RequestOptions { OfferThroughput = 1000 });
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