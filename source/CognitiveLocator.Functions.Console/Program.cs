using CognitiveLocator.Helpers;
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
                Colorful.Console.WriteLine("5.- Request new token to get mobile settings", Color.FromArgb(r, g, b));
                r -= 18; b -= 9;
                Colorful.Console.WriteLine("6.- Request using an existing token to get mobile settings", Color.FromArgb(r, g, b));
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
                        string sresult = await RequestMobileSettings(string.Empty);
                        System.Console.WriteLine(sresult);
                        System.Console.ReadKey();
                        break;

                    case "6":
                        System.Console.Write("\nCurrent token:");
                        string currentToken = System.Console.ReadLine();
                        string fresult = await RequestMobileSettings(currentToken);
                        System.Console.WriteLine(fresult);
                        System.Console.ReadKey();
                        break;

                    case "7":
                        MetadataVerification metadata = new MetadataVerification();
                        metadata.ReportedBy = "Roberto Cervantes";
                        metadata.Country = "MX";
                        metadata.Name = "Ang";
                        metadata.Lastname = "Jol";
                        
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
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            var token = Convert.ToBase64String(time.Concat(key).ToArray());
            token = SecurityHelper.Encrypt(token, Settings.CryptographyKey);
            System.Console.WriteLine($"Token: {token}");

            MetadataVerificationRequest request = new MetadataVerificationRequest();
            request.Token = token;
            request.Metadata = metadata;

            using (var client = new HttpClient())
            {
                var service = $"http://localhost:7071/api/MetadataVerification/";
                byte[] byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request));
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

        public static async Task<string> RequestMobileSettings(string existingToken)
        {
            string token = string.Empty;

            if (string.IsNullOrEmpty(existingToken))
            {
                byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                byte[] key = Guid.NewGuid().ToByteArray();
                token = Convert.ToBase64String(time.Concat(key).ToArray());
                token = SecurityHelper.Encrypt(token, Settings.CryptographyKey);
                System.Console.WriteLine($"Token: {token}");
            }
            else
            {
                token = existingToken;
            }

            MobileSettingsRequest request = new MobileSettingsRequest();
            request.Token = token;

            using (var client = new HttpClient())
            {
                var service = $"http://localhost:7071/api/MobileSettings/";
                byte[] byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request));
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

        private static async Task UploadImageAsync(int option, bool isVerification)
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

            var person = new Person
            {
                Country = "MX",
                ReportedBy = "Roberto Cervantes",
                Name = "Liam",
                Lastname = "Neeson",
                LocationOfLoss = "La Roma",
                DateOfLoss = "20-07-2017",
                ReportId = "675216731",
                Genre = "M",
                Complexion = "Delgada",
                Skin = "Morena",
                Front = "Amplia",
                Mouth = "Grande",
                Eyebrows = "Largas",
                Age = "42",
                Height = "1.80",
                Face = "Delgada",
                Nose = "Aguilena",
                Lips = "Delgados",
                Chin = "Corto",
                TypeColorEyes = "Cafe",
                TypeColorHair = "Negro corto",
                ParticularSigns = "Un tatuaje en la espalda",
                Clothes = "Pantalon de mezclilla"
            };

            var fullPath = Path.Combine(Settings.ImagesPath, file);
            byte[] byteArray = File.ReadAllBytes(fullPath);
            Stream stream = new MemoryStream(byteArray);

            var pid = Guid.NewGuid().ToString();
            if (!isVerification)
            {
                if (await StorageHelper.UploadMetadata(pid, person))
                {
                    if (await StorageHelper.UploadPhoto(pid, stream, isVerification))
                    {
                        System.Console.WriteLine("Carga satisfactoria.");
                    }
                    else
                    {
                        System.Console.WriteLine("No fue posible registrar el reporte, si el error persiste intenta más tarde.");
                    }
                }
                else
                {
                    System.Console.WriteLine("No fue posible registrar el reporte, si el error persiste intenta más tarde.");
                }
            }
            else
            {
                if (await StorageHelper.UploadPhoto(pid, stream, isVerification))
                {
                    System.Console.WriteLine("Carga satisfactoria.");
                }
                else
                {
                    System.Console.WriteLine("No fue posible registrar el reporte, si el error persiste intenta más tarde.");
                }
            }
        }
    }
}