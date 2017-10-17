using Newtonsoft.Json;

namespace CognitiveLocator.Domain
{
    public class Person
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("birthdate")]
        public string BirthDate { get; set; }

        [JsonProperty("reported_by")]
        public string ReportedBy { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("is_active")]
        public int IsActive { get; set; }

        [JsonProperty("is_found")]
        public int IsFound { get; set; }

        [JsonProperty("faceapi_faceid")]
        public string FaceAPI_FaceId { get; set; }

        [JsonProperty("faceapi_personid")]
        public string FaceAPI_PersonId { get; set; }

        [JsonProperty("pending_to_be_deleted")]
        public bool PendingToBeDeleted { get; set; }
    }
}