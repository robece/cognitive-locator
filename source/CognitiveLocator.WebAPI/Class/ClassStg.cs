using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace CognitiveLocator.WebAPI.Class
{
    public class ClassStg
    {

        private CloudStorageAccount CloudStorageAccount;
        private CloudBlobClient CloudBlobClient;
        private CloudBlobContainer CloudBlobContainer;
        private CloudBlockBlob CloudBlockBlob;

        public async Task<string> UploadFile(Stream fileStream, string fileName, string container, string connectionString, string fileExtension = ".jpg")
        {
            fileName = fileName.Replace("\"", "");
            fileStream.Seek(0, SeekOrigin.Begin);
            this.CloudStorageAccount = this.GetCloudStorageAccount(connectionString);
            this.CloudBlobClient = GetCloudBlobClient(CloudStorageAccount);
            this.CloudBlobContainer = GetCloudBlobContainer(CloudBlobClient, container);
            this.CloudBlockBlob = CloudBlobContainer.GetBlockBlobReference(fileName);
            this.CloudBlockBlob.UploadFromStream(fileStream);
            string uri = this.CloudBlockBlob.Uri.ToString();
            return uri;
        }

        private CloudStorageAccount GetCloudStorageAccount(string account)
        {
            return CloudStorageAccount.Parse(account);
        }
        private CloudBlobClient GetCloudBlobClient(CloudStorageAccount account)
        {
            return account.CreateCloudBlobClient();
        }
        private CloudBlobContainer GetCloudBlobContainer(CloudBlobClient blob, string container)
        {
            CloudBlobContainer c = blob.GetContainerReference(container);
            c.CreateIfNotExists();
            c.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            return blob.GetContainerReference(container);
        }

        public async Task<string> UploadPhoto(Stream fileStream, string fileName)
        {
            string container = ConfigurationManager.AppSettings["StgContainer"].ToString();
            string connectionString = ConfigurationManager.AppSettings["StgConnectionString"].ToString();
            var blobUri = await UploadFile(fileStream, fileName, container, connectionString);
            return blobUri;
        }
    }
}