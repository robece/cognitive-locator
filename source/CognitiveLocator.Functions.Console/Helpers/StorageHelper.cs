using CognitiveLocator.Domain;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using CognitiveLocator.Functions.Console;

namespace CognitiveLocator.Helpers
{
    public class StorageHelper
    {
        #region Upload Photo

        public static async Task<bool> UploadPhoto(string identifier, Stream stream, bool IsVerification = false)
        {
            var upload = await ProcessUploadPhoto(stream, $"{identifier}.jpg", IsVerification);

            return !string.IsNullOrEmpty(upload);
        }

        public static async Task<string> ProcessUploadPhoto(Stream stream, string fileName, bool IsVerification)
        {
            var container = IsVerification ? "verification" : "images";
            var connectionString = Settings.AzureWebJobsStorage;

            return UploadFile(stream, fileName, container, connectionString, "image/jpeg", true);
        }

        #endregion

        #region Upload Metadata

        public static async Task<bool> UploadMetadata(string identifier, Person person)
        {
            var str_person = JsonConvert.SerializeObject(person);
            string result = string.Empty;
            using (Stream stream = GetStream(str_person))
            {
                result = await ProcessUploadMetadata(stream, $"{identifier}.json", person);
            }

            return !string.IsNullOrEmpty(result);
        }

        public static async Task<string> ProcessUploadMetadata(Stream stream, string fileName, Person person)
        {
            var container = "metadata";
            var connectionString = Settings.AzureWebJobsStorage;

            return UploadFile(stream, fileName, container, connectionString, "application/json", false);
        }

        public static Stream GetStream(string jsonString)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(jsonString);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        #endregion

        #region Common Methods

        private static CloudStorageAccount GetCloudStorageAccount(string connectionString)
        {
            return CloudStorageAccount.Parse(connectionString);
        }

        private static CloudBlobClient GetCloudBlobClient(CloudStorageAccount account)
        {
            return account.CreateCloudBlobClient();
        }

        private static CloudBlobContainer GetCloudBlobContainer(CloudBlobClient client, string container, bool isImage)
        {
            var c = client.GetContainerReference(container);
            c.CreateIfNotExistsAsync();
            if (isImage)
                c.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            return client.GetContainerReference(container);
        }

        private static string UploadFile(Stream stream, string filename,
                                                           string container, string connectionString,
                                                           string contentType, bool isImage)
        {
            CloudStorageAccount cloudStorageAccount = GetCloudStorageAccount(connectionString);
            CloudBlobClient blobClient = GetCloudBlobClient(cloudStorageAccount);
            CloudBlobContainer blobContainer = GetCloudBlobContainer(blobClient, container, isImage);
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(filename);

            filename = filename.Replace("\"", "");
            stream.Seek(0, SeekOrigin.Begin);

            blockBlob = blobContainer.GetBlockBlobReference(filename);

            blockBlob.Properties.ContentType = contentType;
            blockBlob.UploadFromStream(stream);

            return blockBlob.Uri.ToString();
        }

        #endregion
    }
}