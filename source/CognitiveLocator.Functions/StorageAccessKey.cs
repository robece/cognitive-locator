using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.IO;
using System.Security.Cryptography;
using CognitiveLocator.Common;

namespace CognitiveLocator.Functions
{
    public static class StorageAccessKey
    {
        [FunctionName("StorageAccessKey")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "StorageAccessKey/")]HttpRequestMessage req, TraceWriter log)
        {
            var azureWebJobsStorage = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            var cryptographyKey = Environment.GetEnvironmentVariable("Cryptography_Key");

            Domain.AccessStorageToken accessStorageToken = await req.Content.ReadAsAsync<Domain.AccessStorageToken>();

            var decrypted_token = CryptoManager.Decrypt(accessStorageToken.Token, cryptographyKey);

            byte[] data = Convert.FromBase64String(decrypted_token);
            DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
            if (when < DateTime.UtcNow.AddMinutes(-1))
            {
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            return req.CreateResponse(HttpStatusCode.OK, azureWebJobsStorage);
        }
    }
}
