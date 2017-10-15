using Newtonsoft.Json;

namespace CognitiveLocator.Functions.Models
{
    public class Person
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("_name")]
        public string Name { get; set; }

        [JsonProperty("_lastname")]
        public string LastName { get; set; }

        [JsonProperty("_alias")]
        public string Alias { get; set; }

        [JsonProperty("_birthdate")]
        public string BirthDate { get; set; }

        [JsonProperty("_reportedby")]
        public string ReportedBy { get; set; }

        [JsonProperty("_picture")]
        public string Picture { get; set; }

        [JsonProperty("_location")]
        public string Location { get; set; }

        [JsonProperty("_country")]
        public string Country { get; set; }

        [JsonProperty("_notes")]
        public string Notes { get; set; }

        [JsonProperty("_isactive")]
        public int IsActive { get; set; }

        [JsonProperty("_isfound")]
        public int IsFound { get; set; }

        [JsonProperty("_faceapi_faceid")]
        public string FaceAPI_FaceId { get; set; }

        [JsonProperty("_faceapi_personid")]
        public string FaceAPI_PersonId { get; set; }

        [JsonProperty("_pending_to_be_deleted")]
        public bool PendingToBeDeleted { get; set; }
    }
}