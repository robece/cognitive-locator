using CognitiveLocator.Domain;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CognitiveLocator.Helpers
{
    public class StorageHelper
    {
        public static async Task<bool> UploadPhoto(Stream stream, Person person, bool IsVerification = false)
        {
            var upload = await UploadPhoto(stream, $"{Guid.NewGuid().ToString()}.jpg", person, IsVerification);

            return !string.IsNullOrEmpty(upload);
        }

        public static async Task<string> UploadPhoto(Stream stream, string fileName, Person person, bool IsVerification)
        {
            var container = IsVerification ? "verification" : "images";
            var connectionString = Settings.AzureWebJobsStorage;

            return await UpoloadFileAsync(stream, fileName, person, container, connectionString);
        }

        private static CloudStorageAccount GetCloudStorageAccount(string connectionString)
        {
            return CloudStorageAccount.Parse(connectionString);
        }

        private static CloudBlobClient GetCloudBlobClient(CloudStorageAccount account)
        {
            return account.CreateCloudBlobClient();
        }

        private static async Task<CloudBlobContainer> GetCloudBlobContainer(CloudBlobClient client, string container)
        {
            var c = client.GetContainerReference(container);
            await c.CreateIfNotExistsAsync();
            await c.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            return client.GetContainerReference(container);
        }

        private static async Task<string> UpoloadFileAsync(Stream stream, string fileName, Person person, string container, string connectionString)
        {
            CloudStorageAccount cloudStorageAccount = GetCloudStorageAccount(connectionString);
            CloudBlobClient blobClient = GetCloudBlobClient(cloudStorageAccount);
            CloudBlobContainer blobContainer = await GetCloudBlobContainer(blobClient, container);
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(fileName);

            fileName = fileName.Replace("\"", "");
            stream.Seek(0, SeekOrigin.Begin);

            blockBlob = blobContainer.GetBlockBlobReference(fileName);

            if (container == "images")
            {
                var metadataDictionary = person.ToMetadata();

                foreach (var item in metadataDictionary)
                {
                    blockBlob.Metadata[item.Key] = item.Value;
                }
            }

            blockBlob.Properties.ContentType = "image/jpeg";
            await blockBlob.UploadFromStreamAsync(stream);

            return blockBlob.Uri.ToString();
        }
    }
}