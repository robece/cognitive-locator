using System;
using System.Collections.Generic;

namespace CognitiveLocator.Models
{
    public class Person
    {
        public string IdPerson { get; set; }
        public string Name { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }
        public string Alias { get; set; }
		public string Picture { get; set; }
		public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
		public string Notes { get; set; }
		public string Source { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
        public int IsFound { get; set; }
		public int IsActive { get; set; }
        public int PersonID { get; set; }
        public string FaceID { get; set; }
        public DateTime BirthDate { get; set; }
        public string ReportedBy { get; set; }

        public Dictionary<string, string> ToMetadata()
        {
            return new Dictionary<string, string>
            {
                {"Name", Name},
                {"LastName", LastName},
                {"Age", Age.ToString()},
                {"Alias", Alias},
                {"Location", Location},
                {"Latitude", Latitude.ToString()},
                {"Longitude", Longitude.ToString()},
                {"Notes", Notes},
                {"ReportedBy", ReportedBy}
            };
        }
	}
}
