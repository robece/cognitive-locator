using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveLocator.Functions.Helpers
{
    public class StorageHelper
    {
        private static CloudStorageAccount GetCloudStorageAccount(string connectionString)
        {
            return CloudStorageAccount.Parse(connectionString);
        }

        private static CloudBlobClient GetCloudBlobClient(CloudStorageAccount account)
        {
            return account.CreateCloudBlobClient();
        }

        private static async Task<CloudBlobContainer> GetCloudBlobContainer(CloudBlobClient client, string container, bool isImage)
        {
            var c = client.GetContainerReference(container);
            await c.CreateIfNotExistsAsync();
            if (isImage)
                await c.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            return client.GetContainerReference(container);
        }

        public static async Task<CloudBlockBlob> GetBlockBlob(string filename, string connectionString, string container, bool isImage)
        {
            CloudStorageAccount cloudStorageAccount = GetCloudStorageAccount(connectionString);
            CloudBlobClient blobClient = GetCloudBlobClient(cloudStorageAccount);
            CloudBlobContainer blobContainer = await GetCloudBlobContainer(blobClient, container, isImage);
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(filename);
            return blockBlob;
        }
    }
}
