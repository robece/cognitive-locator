using System;
using System.IO;
using System.Threading.Tasks;
using CognitiveLocator.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace CognitiveLocator.Services
{
    public class StorageManager
    {

        public static async Task PerformBlobOperation()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=your_account_name_here;AccountKey=your_account_key_here");

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");

            // Create the container if it doesn't already exist.
            await container.CreateIfNotExistsAsync();

            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob");

            // Create the "myblob" blob with the text "Hello, world!"
            await blockBlob.UploadTextAsync("Hello, world!");
        }

        public static async Task<bool> UploadPhoto(Stream stream, Person person, bool IsVerification = false)
        {
            var upload = await UploadPhoto(stream, $"{Guid.NewGuid().ToString()}.jpg", person, IsVerification);

            return !string.IsNullOrEmpty(upload);
        }

        public static async Task<string> UploadPhoto(Stream stream, string fileName, Person person, bool IsVerification)
        {
            var container = IsVerification ? "verification" : "images";
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=cognitivelocatordev;AccountKey=juIB8wTfGeS+OlZZctPXr39i5MUvO9AL8Ds4t1bloUvF5J5c3Y6dOxqEwqzYsAcTReekS4GsF9+CfaMnIWTceA==";

            return await UpoloadFileAsync(stream, fileName, person, container, connectionString);
        }

        static CloudStorageAccount GetCloudStorageAccount(string connectionString)
        {
            return CloudStorageAccount.Parse(connectionString);
        }

        static CloudBlobClient GetCloudBlobClient(CloudStorageAccount account)
        {
            return account.CreateCloudBlobClient();
        }

        static async Task<CloudBlobContainer> GetCloudBlobContainer(CloudBlobClient client, string container)
        {
            var c = client.GetContainerReference(container);
            await c.CreateIfNotExistsAsync();
            await c.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            return client.GetContainerReference(container);
        }

        static async Task<string> UpoloadFileAsync(Stream stream, string fileName, Person person, string container, string connectionString)
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
