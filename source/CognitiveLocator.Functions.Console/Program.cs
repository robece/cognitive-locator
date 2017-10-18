using CognitiveLocator.Common;
using CognitiveLocator.Domain;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveLocator.Functions.Console
{
    public class Program
    {
        private static string azureWebJobsStorage = "";
        private static string cryptographyKey = "";

        private static void Main(string[] args)
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

                int r = 225;
                int g = 255;
                int b = 250;

                Colorful.Console.WriteAscii("COGNITIVE LOCATOR", Color.FromArgb(r, g, b));

                Colorful.Console.WriteLine("1.- Register person", Color.FromArgb(r, g, b));
                r -= 18; b -= 9;
                Colorful.Console.WriteLine("2.- Verificate person", Color.FromArgb(r, g, b));
                r -= 18; b -= 9;
                Colorful.Console.WriteLine("3.- Upload wrong person image", Color.FromArgb(r, g, b));
                r -= 18; b -= 9;
                Colorful.Console.WriteLine("4.- Upload multiple face image", Color.FromArgb(r, g, b));
                r -= 18; b -= 9;
                Colorful.Console.WriteLine("5.- Request new token to get current storage access key", Color.FromArgb(r, g, b));
                r -= 18; b -= 9;
                Colorful.Console.WriteLine("6.- Request using an existing token to get current storage access key", Color.FromArgb(r, g, b));
                r -= 18; b -= 9;
                Colorful.Console.WriteLine("7.- Search document by metadata attribute", Color.FromArgb(r, g, b));
                r -= 18; b -= 9;
                Colorful.Console.WriteLine("8.- Exit", Color.FromArgb(r, g, b));
                r -= 18; b -= 9;

                Colorful.Console.Write("\nPick an option:", Color.FromArgb(r, g, b));
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

                    case "5":
                        string sresult = await RequestStorageToken(string.Empty);
                        System.Console.WriteLine(sresult);
                        System.Console.ReadKey();
                        break;

                    case "6":
                        System.Console.Write("\nCurrent token:");
                        string currentToken = System.Console.ReadLine();
                        string fresult = await RequestStorageToken(currentToken);
                        System.Console.ReadKey();
                        break;
                    case "7":
                        MetadataVerification metadata = new MetadataVerification();
                        metadata.Name = "Ro";
                        metadata.Country = "MEX";
                        string sdresult = await SearchDocument(metadata);
                        System.Console.WriteLine(sdresult);
                        System.Console.ReadKey();
                        break;
                }
            }
            while (option != "8");
        }

        public static async Task<string> SearchDocument(MetadataVerification metadata)
        {
            using (var client = new HttpClient())
            {
                var service = $"http://localhost:7071/api/MetadataVerification/";
                byte[] byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(metadata));
                using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var httpResponse = client.PostAsync(service, content).Result;

                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        System.Console.WriteLine(httpResponse.StatusCode);
                        return await httpResponse.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        System.Console.WriteLine(httpResponse.StatusCode);
                    }
                }
            }
            return null;
        }

        public static async Task<string> RequestStorageToken(string existingToken)
        {
            string token = string.Empty;

            if (string.IsNullOrEmpty(existingToken))
            {
                byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                byte[] key = Guid.NewGuid().ToByteArray();
                token = Convert.ToBase64String(time.Concat(key).ToArray());
                token = CryptoManager.Encrypt(token, cryptographyKey);
                System.Console.WriteLine($"Token: {token}");
            }
            else
            {
                token = existingToken;
            }

            using (var client = new HttpClient())
            {
                var service = $"http://localhost:7071/api/StorageAccessKey/";
                byte[] byteData = Encoding.UTF8.GetBytes("{'Token':'" + token + "'}");
                using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var httpResponse = client.PostAsync(service, content).Result;

                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        System.Console.WriteLine(httpResponse.StatusCode);
                        string result = JsonConvert.DeserializeObject<string>(await httpResponse.Content.ReadAsStringAsync());
                        return result;
                    }
                    else
                    {
                        System.Console.WriteLine(httpResponse.StatusCode);
                    }
                }
            }
            return null;
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
            string connectionString = azureWebJobsStorage;
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
            CloudBlockBlob.Metadata["birthdate"] = "birthdate";
            CloudBlockBlob.Metadata["reportedby"] = "cervantes";

            CloudBlockBlob.Properties.ContentType = "image/jpeg";
            CloudBlockBlob.UploadFromStream(fileStream);

            string uri = CloudBlockBlob.Uri.ToString();
            return uri;
        }
    }
}