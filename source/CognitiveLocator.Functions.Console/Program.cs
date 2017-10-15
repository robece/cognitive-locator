using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveLocator.Functions.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            PrintMenu();
        }

        private static async void PrintMenu()
        {
            string option = string.Empty;
            do
            {
                option = string.Empty;
                System.Console.Clear();
                System.Console.WriteLine("1.- Upload a person Image");
                System.Console.WriteLine("2.- Upload a verification person Image");
                System.Console.WriteLine("3.- Upload a wrong person Image");
                System.Console.WriteLine("4.- Upload a multiple face Image");
                System.Console.WriteLine("5.- Exit");

                System.Console.Write("\nPick an option:");
                option = System.Console.ReadLine();

                switch (option)
                {
                    case "1":
                        await UploadImageAsync(0, false);
                        break;

                    case "2":
                        await UploadImageAsync(0, true);
                        break;

                    case "3":
                        await UploadImageAsync(1, false);
                        break;

                    case "4":
                        await UploadImageAsync(2, false);
                        break;
                }

            }
            while (option != "5");
        }

        private static async Task<string> UploadImageAsync(int option, bool isVerification)
        {
            var file = string.Empty;
            switch (option)
            {
                case 0:
                    file = @"Images\Person.jpg";
                    break;
                case 1:
                    file = @"Images\NoFace.jpg";
                    break;
                case 2:
                    file = @"Images\Family.jpg";
                    break;
            }

            var fullPath = Path.Combine(@"C:\GitHub\cognitive-locator\source\CognitiveLocator.Functions.Console", file);
            byte[] byteArray = File.ReadAllBytes(fullPath);
            Stream stream = new MemoryStream(byteArray);
            return await UploadPhotoAsync(stream, Guid.NewGuid().ToString() + ".jpg", isVerification);
        }

        private static async Task<string> UploadPhotoAsync(Stream fileStream, string fileName, bool isVerification)
        {
            string container = (isVerification) ? "verification" : "images";
            string connectionString = "__CONNECTION STRING__";
            var blobUri = await UploadFileAsync(fileStream, fileName, container, connectionString);
            return blobUri;
        }

        private static CloudStorageAccount GetCloudStorageAccount(string account)
        {
            return CloudStorageAccount.Parse(account);
        }

        private static CloudBlobClient GetCloudBlobClient(CloudStorageAccount account)
        {
            return account.CreateCloudBlobClient();
        }

        private static CloudBlobContainer GetCloudBlobContainer(CloudBlobClient blob, string container)
        {
            CloudBlobContainer c = blob.GetContainerReference(container);
            c.CreateIfNotExists();
            c.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            return blob.GetContainerReference(container);
        }

        private static async Task<string> UploadFileAsync(Stream fileStream, string fileName, string container, string connectionString, string fileExtension = ".jpg")
        {
            CloudStorageAccount CloudStorageAccount = null;
            CloudBlobClient CloudBlobClient = null;
            CloudBlobContainer CloudBlobContainer = null;
            CloudBlockBlob CloudBlockBlob = null;

            fileName = fileName.Replace("\"", "");
            fileStream.Seek(0, SeekOrigin.Begin);
            CloudStorageAccount = GetCloudStorageAccount(connectionString);
            CloudBlobClient = GetCloudBlobClient(CloudStorageAccount);
            CloudBlobContainer = GetCloudBlobContainer(CloudBlobClient, container);
            CloudBlockBlob = CloudBlobContainer.GetBlockBlobReference(fileName);
            CloudBlockBlob.Metadata["country"] = "MEX";
            CloudBlockBlob.Metadata["name"] = "roberto";
            CloudBlockBlob.Metadata["lastname"] = "cervantes";
            CloudBlockBlob.Metadata["location"] = "location";
            CloudBlockBlob.Metadata["notes"] = "notes";
            CloudBlockBlob.Metadata["alias"] = "alias";
            CloudBlockBlob.Metadata["birthday"] = "birthday";
            CloudBlockBlob.Metadata["reportedby"] = "cervantes";
            CloudBlockBlob.Metadata["lastname"] = "cervantes";
            CloudBlockBlob.Metadata["device_hash"] = "device_hash";

            CloudBlockBlob.Properties.ContentType = "image/jpeg";
            CloudBlockBlob.UploadFromStream(fileStream);

            string uri = CloudBlockBlob.Uri.ToString();
            return uri;
        }
    }
}
