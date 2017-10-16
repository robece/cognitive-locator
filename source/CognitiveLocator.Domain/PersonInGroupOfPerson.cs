using Newtonsoft.Json;

namespace CognitiveLocator.Domain
{
    public class PersonInGroupOfPerson
    {
        [JsonProperty("persistedFaceIds")]
        public string[] PersistedFaceIds { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("personId")]
        public string PersonId { get; set; }

        [JsonProperty("userData")]
        public string UserData { get; set; }
    }
}