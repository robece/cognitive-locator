using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CognitiveLocator.WebAPI.Class
{
    public class ClassStg
    {

        private CloudStorageAccount CloudStorageAccount;
        private CloudBlobClient CloudBlobClient;
        private CloudBlobContainer CloudBlobContainer;
        private CloudBlockBlob CloudBlockBlob;

        public async Task<string> UploadFile(Stream fileStream, string fileName, string container, string storageAccount, string fileExtension = ".jpg")
        {
            fileStream.Seek(0, SeekOrigin.Begin);
            this.CloudStorageAccount = this.GetCloudStorageAccount(storageAccount);
            this.CloudBlobClient = GetCloudBlobClient(CloudStorageAccount);
            this.CloudBlobContainer = GetCloudBlobContainer(CloudBlobClient, container);
            this.CloudBlockBlob = CloudBlobContainer.GetBlockBlobReference(fileName + fileExtension);
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



    }
}