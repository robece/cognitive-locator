using Microsoft.Azure.NotificationHubs;
using System;

namespace CognitiveLocator.Functions.Client
{
    public class NotificationClient
    {
        public static NotificationClient Instance = new NotificationClient();

        public NotificationHubClient Hub { get; set; }

        private string AccessSignature = Environment.GetEnvironmentVariable("NotificationHub_Access_Signature");
        private string HubName = Environment.GetEnvironmentVariable("NotificationHub_Name");

        private NotificationClient()
        {
            Hub = NotificationHubClient.CreateClientFromConnectionString(AccessSignature, HubName);
        }
    }
}