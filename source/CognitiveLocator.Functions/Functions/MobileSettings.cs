using CognitiveLocator.Common;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
<<<<<<< HEAD
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
=======
using System.IO;
using System.Security.Cryptography;
using CognitiveLocator.Functions.Helpers;
using System.Collections.Generic;
using CognitiveLocator.Domain.Templates;
using Newtonsoft.Json;
>>>>>>> master

namespace CognitiveLocator.Functions
{
    public static class MobileSettings
    {
        [FunctionName(nameof(MobileSettings))]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "MobileSettings/")]HttpRequestMessage req, TraceWriter log)
        {
            Domain.MobileSettingsRequest request = await req.Content.ReadAsAsync<Domain.MobileSettingsRequest>();

            var decrypted_token = SecurityHelper.Decrypt(request.Token, Settings.CryptographyKey);

            byte[] data = Convert.FromBase64String(decrypted_token);
            DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
            if (when < DateTime.UtcNow.AddMinutes(-5))
            {
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add(nameof(Settings.AzureWebJobsStorage), Settings.AzureWebJobsStorage);
            result.Add(nameof(Settings.MobileCenterID_Android), Settings.MobileCenterID_Android);
            result.Add(nameof(Settings.MobileCenterID_iOS), Settings.MobileCenterID_iOS);
            result.Add(nameof(Settings.ImageStorageUrl), Settings.ImageStorageUrl);
            return req.CreateResponse(HttpStatusCode.OK, result);
        }

        private static string GenerateRegistrationTemplate()
        {
            Registration registration = new Registration();
            registration.Version = "1.0";
            Registration.TableSection section1 = new Registration.TableSection();
            section1.Title = "Custom Section";

            Registration.Label label = new Registration.Label();
            label.Name = "name";
            label.Text = "Nombre:";
            label.Margin = "40,20,40,0";
            label.FontSize = "Medium";
            label.TextColor = "Gray";
            section1.Elements.Add(label);

            registration.Sections.Add(section1);
            return JsonConvert.SerializeObject(registration);
        }
    }
}